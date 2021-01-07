using System.Collections.Generic;

namespace Homedish.Dtos
{
    public class AggregateProduct
    {
        public IList<Product> Products { get; set; }
        public IList<Special> Specials { get; set; }
    }
}
