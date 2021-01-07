using System.Collections.Generic;

namespace Homedish.Dtos
{
    public class Special
    {
        public IList<SpecialProduct> Products { get; set; }
        public int Total { get; set; }
    }
}
