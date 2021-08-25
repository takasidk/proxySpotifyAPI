using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyProxyAPI.Models;

namespace SpotifyProxyAPI.Repositories.Interfaces
{
    /// <summary>
    /// Data repository Interface 
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        /// Async method which returns accesstoken when expired
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public  Task<string> GetAccesstokenAsync(string clientId, string clientSecret);

        /// <summary>
        /// Async method which returns top 5 artists information and caches to database if required
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public Task<ActionResult> GetItemsAsync(ItemRequest itemRequest);

        /// <summary>
        /// Async method which checks the health of the API
        /// </summary>
        /// <returns></returns>
        public  Task<bool> IsAliveAsync();

    }
}
