using Microsoft.Extensions.Options;
using MTGApi.Helpers;
using MTGApi.Models.MTGApi;
using Newtonsoft.Json;
using RestSharp;

namespace MTGApi.APIs
{
    public interface IMTGClient
    {
        IEnumerable<Card>? GetCards(int pageNumber, int pageSize);
        Card? GetCardById(string id);
    }
    public class MTGClient : IMTGClient
    {
        private readonly MTGApiSettings _settings;
        private readonly RestClient _client;
        public MTGClient(IOptions<MTGApiSettings> options)
        {
            _settings = options.Value;
            _client = new RestClient(baseUrl: _settings.BaseUrl);
        }


        public IEnumerable<Card>? GetCards(int pageNumber, int pageSize)
        {
            var request = new RestRequest("/cards", Method.Get);
            request.AddQueryParameter("page", pageNumber);
            request.AddQueryParameter("pageSize", pageSize);

            var response = _client.ExecuteGet(request);

            if (!response.IsSuccessful)
                throw new Exception("Erro");

            var cardsResponse = JsonConvert.DeserializeObject<CardResponse>(response.Content);
            return cardsResponse?.Cards;
        }

        public Card? GetCardById(string id)
        {
            var request = new RestRequest("/cards", Method.Get);
            request.AddQueryParameter("id", id);

            var response = _client.ExecuteGet(request);

            if (!response.IsSuccessful)
                throw new Exception("Erro");

            var cardResponse = JsonConvert.DeserializeObject<CardResponse>(response.Content);
            return cardResponse?.Cards[0];
        }
    }
}
