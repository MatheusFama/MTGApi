using Microsoft.AspNetCore.Mvc;
using MTGApi.Models.MTGApi;
using MTGApi.Services;

namespace MTGApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : BaseController
    {
        private readonly IMTGApiService _mtgapiService;

        public CardController(IMTGApiService mtgapiService)
        {
            _mtgapiService = mtgapiService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Card>> GetCards([FromQuery] int page)
        {
            var cards = _mtgapiService.GetCards(page, 100);

            return Ok(cards);
        }
    }
}
