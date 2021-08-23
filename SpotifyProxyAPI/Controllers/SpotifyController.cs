using Microsoft.AspNetCore.Mvc;
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
        private readonly IDataRepository _dataRepository;
        public SpotifyController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        
        /// <summary>
        /// Asynchronous method which calls GetItems Repository method
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        [HttpPost("getArtists")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ModelValidationFilter]

        public async Task<ActionResult> GetArtistsAsync([FromBody] ItemRequest itemRequest)
        {   
            return await _dataRepository.GetItemsAsync(itemRequest);
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        /// <returns></returns>
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
