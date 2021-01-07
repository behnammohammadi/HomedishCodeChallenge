using System.Threading.Tasks;
using Homedish.Dtos;
using Homedish.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homedish.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("/total")]
        public int Calculate(AggregateProduct products)
        {
            return _basketService.CalculateBasketPrice(products);
        }

        [HttpGet("/total")]
        public async ValueTask<int> CalculateAsync()
        {
            var products = await _basketService.RetriveProductsAsync();
            return _basketService.CalculateBasketPrice(products);
        }
    }
}
