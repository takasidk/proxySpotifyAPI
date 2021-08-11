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

namespace SpotifyProxyAPI.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly IMongoCollection<ResponseDTO> _data;
        private readonly IOptions<DatabaseSettings> _settings;
        private readonly IOptions<UserSettings> _config;
        private readonly IHttpClientFactory _clientFactory;
        private DateTime ExpireTime = DateTime.Now;
        private string AccessToken;
        public DataRepository(IOptions<DatabaseSettings> settings, IOptions<UserSettings> config, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _config = config;
            _settings = settings;
            var connection = new MongoClient(_settings.Value.ConnectionString);
            var database = connection.GetDatabase(_settings.Value.DatabaseName);

            _data = database.GetCollection<ResponseDTO>(_settings.Value.CollectionName);
        }

        public async Task<string> GetAccesstoken(string clientId, string clientSecret)
        {
            if (DateTime.Now >= ExpireTime)
            {
                Log.Information("Using OAuth Endpoint to get Access Token");
                var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

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
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var authResult = await System.Text.Json.JsonSerializer.DeserializeAsync<AuthResponse>(responseStream);
                AccessToken = authResult.access_token;

            }
            return AccessToken;
        }
        public async Task<ActionResult> GetItems(ItemRequest itemRequest, string accessToken)

        {
            List<ResponseDTO> myList = new List<ResponseDTO>();
            var query = itemRequest.Query;
            var filter = new BsonDocument("Query", query);
            using (MiniProfiler.Current.Step("Time taken to retrieve data from database"))
            {
                myList = _data.Find(filter).ToList();
            }
            if (myList.Count == 0)
            {
                Log.Information("Getting Top 5 Artists List from Spotify Item Search Endpoint");
                var client = _clientFactory.CreateClient();

                client.BaseAddress = new Uri("https://api.spotify.com/v1/");
                // client.DefaultRequestHeaders.Add("Accept", "application/json");
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync($"search?q={query}&type=artist&market={itemRequest.market}");//&offset=5&limit=5

                if (response.StatusCode==HttpStatusCode.OK)
                {

                    var responseStream = response.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<ItemResponse>(responseStream);

                    var Items = responseObject?.artists?.items.OrderByDescending(i => i.followers.total).Take(5);//.ThenBy(i => i.Popularity);
                    var res = new ResponseDTO
                    {
                        Query = query,
                        Market = itemRequest.market,
                        ArtistsList = Items.Select(i => new Top5Artists
                        {
                            Id = i.id,
                            Name = i.name,
                            Followers = i.followers.total,
                            Popularity = i.popularity,
                            Link = i.external_urls.spotify
                        })
                    };
                    await _data.InsertOneAsync(res);
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(res),
                        ContentType = Constants.JSON_CONTENT,
                        StatusCode = 200
                    };
                }
                else if (response.StatusCode==HttpStatusCode.NotFound)
                {
                    var errorResponse = new ErrorResponse();
                    errorResponse.ErrorMessage = "resource not found";
                    errorResponse.StatusCode = 404;
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(errorResponse),
                        ContentType = Constants.JSON_CONTENT,
                        StatusCode = 404
                    };
                }
            }
            
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(myList[0]),
                ContentType = Constants.JSON_CONTENT,
                StatusCode = 200
            }; 
        }


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
