using System.Threading.Tasks;
using SpotifyProxyAPI.Models;

namespace SpotifyProxyAPI.Repositories.Interfaces
{
    public interface IDataRepository
    {
        public  Task<string> GetAccesstoken(string clientId, string clientSecret);

        public Task<ResponseDTO> GetItems(ItemRequest itemRequest, string accessToken);

        public  Task<bool> IsAliveAsync();

    }
}
