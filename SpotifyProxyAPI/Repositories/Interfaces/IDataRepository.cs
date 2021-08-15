using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyProxyAPI.Models;

namespace SpotifyProxyAPI.Repositories.Interfaces
{
    public interface IDataRepository
    {
        public  Task<string> GetAccesstokenAsync(string clientId, string clientSecret);

        public Task<ActionResult> GetItemsAsync(ItemRequest itemRequest);

        public  Task<bool> IsAliveAsync();

    }
}
