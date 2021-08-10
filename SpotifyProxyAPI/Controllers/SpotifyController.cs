using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Profiling;
using System.Threading.Tasks;
using SpotifyProxyAPI.Helpers;
using SpotifyProxyAPI.Models;
using SpotifyProxyAPI.Repositories.Interfaces;

namespace SpotifyProxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase

    {
        private  readonly  IOptions<UserSettings>_config;
        private readonly IDataRepository _dataRepository;
        public SpotifyController(IOptions<UserSettings> config, IDataRepository dataRepository)
        {
            _config = config;
            _dataRepository = dataRepository;
        }
        

        [HttpPost("getArtists")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ModelValidationFilter]

        public Task<ResponseDTO> getArtists([FromBody] ItemRequest itemRequest)
        {
            var accessToken = _dataRepository.GetAccesstoken(_config.Value.SpotifySettings.ClientId, _config.Value.SpotifySettings.ClientSecret).Result;
            return _dataRepository.GetItems(itemRequest, accessToken);
        }

        #region IsAlive
        [HttpGet("health")]
        public async Task<IActionResult> IsAliveAsync()
        {
            var health = await _dataRepository.IsAliveAsync();
            using (MiniProfiler.Current.Step(Constants.Health))
            {
                Log.Information(Constants.Health);
                return Content(JsonConvert.SerializeObject(health));
            }
        }
        #endregion

    }
}
