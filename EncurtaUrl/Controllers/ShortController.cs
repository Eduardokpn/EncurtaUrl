using EncurtaUrl.Interfaces;
using EncurtaUrl.Interfaces.Repository;
using EncurtaUrl.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EncurtaUrl.Controllers
{


    public class ShortController : Controller
    {
        private readonly IShortRepository _shortRepository;
        public ShortController(IShortRepository shortRepository)
        {
            _shortRepository = shortRepository;

        }



        public IActionResult ShortIndex(ShortClass url)
        {
            return View(url);
        }

        [HttpPost]
        public IActionResult ShortUrl(ShortClass model)
        {
            model.ShortUrl = _shortRepository.CreteEncondig(model.Url);
            _shortRepository.Add(model);


            return View("ShortIndex", model);

        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> GetByShortLink(string shortUrl)
        {
            var longurl = _shortRepository.GetLongUrl(shortUrl);


         return RedirectPermanent(longurl);
        }


        
    }
}
