using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Entities
{
    public class CustumerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
