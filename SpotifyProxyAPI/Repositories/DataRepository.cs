using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SpotifyProxyAPI.Helpers;
using SpotifyProxyAPI.Models;
using SpotifyProxyAPI.Repositories.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SpotifyProxyAPI.Middlewares;

namespace SpotifyProxyAPI.Repositories
{
    /// <summary>
    /// Data respositoy which implements IDataRespository Interface 
    /// </summary>
    public class DataRepository : IDataRepository
    {
        private readonly IMongoCollection<ResponseDto> _data;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IOptions<UserSettings> _config;
        private readonly IOptions<DatabaseSettings> _settings;
        private DateTime ExpireTime = DateTime.Now;
        private string AccessToken;

        /// <summary>
        /// Custom Parameterised Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="clientFactory"></param>
        /// <param name="config"></param>
        public DataRepository(IOptions<DatabaseSettings> settings, IHttpClientFactory clientFactory, IOptions<UserSettings> config)
        {
            _clientFactory = clientFactory;
            _config = config;
            _settings = settings;
            
                var connection = new MongoClient(settings.Value.ConnectionString);
                var database = connection.GetDatabase(settings.Value.DatabaseName);

                _data = database.GetCollection<ResponseDto>(settings.Value.CollectionName);
            
        }

        
        /// <summary>
        /// Async method which calls OAuth endpoint of Spotify if the accesstoken is expired
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public async Task<string> GetAccesstokenAsync(string clientId, string clientSecret)
        {
            //If AccessToken is Expired
            if (DateTime.Now >= ExpireTime)
            {
                Log.Information("Using OAuth Endpoint to get Access Token");
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.Value.SpotifySettings.TokenUri}");

                request.Headers.Authorization = new AuthenticationHeaderValue(
                                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                {"grant_type", "client_credentials"}
                });
                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                ExpireTime = DateTime.Now.AddHours(1);
                 var responseStream =  response.Content.ReadAsStringAsync().Result;
                var authResult =  JsonConvert.DeserializeObject<AuthResponse>(responseStream);
                AccessToken = authResult.Access_token;

            }
            return AccessToken;
        }

        /// <summary>
        /// Async method which calls Item search end point of spotify and returns top 5 artists as a list
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetItemsAsync(ItemRequest itemRequest)

        {
            List<ResponseDto> myList;
            var transId = Guid.NewGuid().ToString();
            if (AuditMiddleware.Logger != null)
            {
                AuditLogger.RequestInfo(
                    transId, Constants.Post, Constants.Path, string.Empty, itemRequest.ToString());
            }
            var query = itemRequest.Query;
            var filter = new BsonDocument("Query", query);
            using (MiniProfiler.Current.Step("Time taken to retrieve data from database"))
            {
                try
                {
                    myList = _data.Find(filter).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (myList.Count == 0)
            {
                var accessToken = await GetAccesstokenAsync(_config.Value.SpotifySettings.ClientId, _config.Value.SpotifySettings.ClientSecret);

                Log.Information("Getting Top 5 Artists List from Spotify Item Search Endpoint");

                var client = _clientFactory.CreateClient("Spotify");
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync($"search?q={query}&type=artist&market=us");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<ItemResponse>(responseStream);

                    var Items = responseObject?.Artists?.Items.OrderByDescending(i => i.Followers.Total).Take(5);
                    if (!Items.Any())
                    {
                        Log.Information("Resouce not found");
                        var errorResponse = new ErrorResponse
                        {
                            ErrorMessage = "resource not found",
                            StatusCode = 404
                        };
                        return new ContentResult
                        {
                            Content = JsonConvert.SerializeObject(errorResponse),
                            ContentType = Constants.JSON_CONTENT,
                            StatusCode = 404
                        };
                    }
                    var res = new ResponseDto
                    {
                        Query = query,

                        Value = Items.Select(i => new Top5Artists
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Followers = i.Followers.Total,
                            Popularity = i.Popularity,
                            Link = i.External_urls.Spotify
                        })
                    };
                    await _data.InsertOneAsync(res);
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(res.Value),
                        ContentType = Constants.JSON_CONTENT,
                        StatusCode = 200
                    };
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    Log.Information("Resouce not found");
                    var errorResponse = new ErrorResponse
                    {
                        ErrorMessage = "Invalid Query",
                        StatusCode = 400
                    };

                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(errorResponse),
                        ContentType = Constants.JSON_CONTENT,
                        StatusCode = 400
                    };
                }
            }
            Log.Information("Value is {0}", myList[0]);
            if (AuditMiddleware.Logger != null)
            {
                AuditLogger.ResponseInfo(transId, Constants.Post, Constants.Path, string.Empty, _settings.Value.DatabaseName, _settings.Value.CollectionName, myList[0].ToString());
            }
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(myList[0]),
                ContentType = Constants.JSON_CONTENT,
                StatusCode = 200
            };
        }


        /// <summary>
        /// Async method which checks health of the API
        /// </summary>
        /// <returns></returns>
        #region
        public async Task<bool> IsAliveAsync()
        {
            try
            {
                using (MiniProfiler.Current.Step(Constants.HealthCommand))
                {
                    await Task.Delay(1);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
            }
            return false;
        }
        #endregion
    }
}
