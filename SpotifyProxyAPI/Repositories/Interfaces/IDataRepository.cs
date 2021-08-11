using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyProxyAPI.Models;

namespace SpotifyProxyAPI.Repositories.Interfaces
{
    public interface IDataRepository
    {
        public  Task<string> GetAccesstoken(string clientId, string clientSecret);

        public Task<ActionResult> GetItems(ItemRequest itemRequest, string accessToken);

        public  Task<bool> IsAliveAsync();

    }
}
