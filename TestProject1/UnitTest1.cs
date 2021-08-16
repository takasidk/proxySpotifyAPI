using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Serilog;
using SpotifyProxyAPI;
using SpotifyProxyAPI.Controllers;
using SpotifyProxyAPI.Helpers;
using SpotifyProxyAPI.Middlewares;
using SpotifyProxyAPI.Models;
using SpotifyProxyAPI.Repositories;
using SpotifyProxyAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {

        IHttpClientFactory _clientFactory;

        IOptions<DatabaseSettings> _options;
        IOptions<UserSettings> _config;
        DataRepository dataRepository;
        SpotifyController controller;
        
        
        [TestInitialize]
        public void InitConfiguration()
        {
             var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var services = new ServiceCollection();
            services.AddHttpClient();
            
            _clientFactory=services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
           
                _options = Options.Create(new DatabaseSettings()
                {
                    ConnectionString = config.GetValue<string>("DatabaseSettings:ConnectionString"),
                    DatabaseName = config.GetValue<string>("DatabaseSettings:DatabaseName"),
                    CollectionName = config.GetValue<string>("DatabaseSettings:CollectionName")
                });
            
            _config = Options.Create(new UserSettings()
            {
                SpotifySettings = new SpotifySettings
                {
                    ClientId = config.GetValue<string>("UserSettings:SpotifySettings:ClientId"),
                    ClientSecret = config.GetValue<string>("UserSettings:SpotifySettings:ClientSecret"),
                    TokenUri = config.GetValue<string>("UserSettings:SpotifySettings:TokenUri"),
                    ItemSearchBaseUri=config.GetValue<string>("UserSettings:SpotifySettings:ItemSearchBaseUri")
                }

            }) ;
            dataRepository = new DataRepository(_options, _clientFactory, _config);
            controller=new SpotifyController(dataRepository);
        }
        


          

        [TestMethod]
        public async Task TestMethod1()

        {
            //Arrange
            var request = new ItemRequest { 
                Query="adel",
            };
            
            //Act
            var response = await controller.GetArtistsAsync(request) as ContentResult;

            //Assert
            Assert.AreEqual(200,response.StatusCode);
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            //Arrange
            var request = new ItemRequest
            {
                Query = "adihaidhhdiahidhiahiancn"
            };
            //Act
            var response = await controller.GetArtistsAsync(request) as ContentResult;
            //Assert
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public async Task  TestMethod3()
        {
            //Arrange
            var request = new ItemRequest
            {
                Query = "#^^&$$$$"
            };
            //Act
            var response = await controller.GetArtistsAsync(request) as ContentResult;
            //Assert
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public async Task TestMethod4()
        {
            //Act
            var response = await controller.IsAliveAsync() as ContentResult;
            //Assert
            Assert.AreEqual("true", response.Content);
        }

      

        [TestMethod]
        public void TestMethod6()
        {
            var ioptions = new Mock<IOptions<DatabaseSettings>>();
            var datarepo = new Mock<DataRepository>(ioptions);
            var validationFilter = new ModelValidationFilterAttribute();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Query", Constants.QUERYFIELD_REQUIRED);

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<SpotifyController>(datarepo)
            );

            validationFilter.OnActionExecuting(actionExecutingContext);
            Assert.IsInstanceOfType(actionExecutingContext.Result, typeof(BadRequestObjectResult));
        }



        [TestMethod]
        public async Task StartUpHealth_Success_Test()
        {
            // Arrange
            var projectDir = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var server = new TestServer(new WebHostBuilder()
                .UseContentRoot(projectDir)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseSerilog());


            // Act
            using var client = server.CreateClient();
            var request = new ItemRequest
            {
                Query = "jack"
            };
            string strPayload = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/Spotify/getArtists", c);
            // Ensure Success StatusCode is returned from response

            result.EnsureSuccessStatusCode();

            // Assert
            Assert.IsTrue(result.IsSuccessStatusCode);
            Assert.AreEqual(200, (int)result.StatusCode);
        }

        [TestMethod]
        public async Task StartUpHealth_error404()
        {
            // Arrange
            var projectDir = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.Development.json")
                .Build();
            

            var server = new TestServer(new WebHostBuilder()
                .UseContentRoot(projectDir)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseSerilog());


            // Act
            using var client = server.CreateClient();
            var request = new ItemRequest
            {
                Query = "@#$$%%^^^$$$"
            };
            string strPayload = JsonConvert.SerializeObject(request);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/Spotify/getArtists", c);
            // Ensure Success StatusCode is returned from response
            await result.Content.ReadAsStringAsync();
            Assert.AreEqual(400, (int)result.StatusCode);
        }
    }
}
