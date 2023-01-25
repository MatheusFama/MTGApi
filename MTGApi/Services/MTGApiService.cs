using MTGApi.APIs;
using MTGApi.Models.MTGApi;

namespace MTGApi.Services
{
    public interface IMTGApiService
    {
        IEnumerable<Card> GetCards(int page, int pageNumber);
    }

    public class MTGApiService : IMTGApiService
    {
        private readonly IMTGClient _api;
        public MTGApiService(IMTGClient api)
        {
            _api = api;
        }

        public IEnumerable<Card> GetCards(int page,int pageNumber)
        {
            return _api.GetCards(page,pageNumber);
        }
    }
}
