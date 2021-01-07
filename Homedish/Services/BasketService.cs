using Homedish.Constants;
using Homedish.Dtos;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Homedish.Services
{
    public interface IBasketService
    {
        Task<AggregateProduct> RetriveProductsAsync();
        int CalculateBasketPrice(AggregateProduct products);
    }

    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(GlobalConstants.HTTP_CLIENT_HOMEDISH);
        }

        public async Task<AggregateProduct> RetriveProductsAsync()
        {
            var response = await _httpClient.GetAsync("products");
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AggregateProduct>(content);

            return result;
        }


        public int CalculateBasketPrice(AggregateProduct products)
        {
            // find offer with greater total.
            var prefredSpecial = products.Specials.FirstOrDefault(x => x.Total == products.Specials.Max(e => e.Total));

            // to remove overlap between specials and our baseket.
            foreach (var item in prefredSpecial.Products)
            {
                var current = products.Products.FirstOrDefault(x => x.Name == item.Name);

                // assume we use greater special even we can't use all of them.
                if (current.Quantity < item.Quantity)
                    current.Quantity = 0;

                current.Quantity -= item.Quantity;
            }

            // calculate basket price without specials.
            int basketPrice = products.Products.Sum(x => x.Quantity * x.Price);

            // sum basket price and specials as result.
            int result = basketPrice + prefredSpecial.Total;
            return result;
        }
    }
}
