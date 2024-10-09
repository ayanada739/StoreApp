using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
        Task<CustumerBasket?> GetBasketAsync(string basketId);
        Task<CustumerBasket?> UpdateBasketAsync(CustumerBasket basket);

        Task<bool> DeleteBasketAsync(string basketId);


    }
}
