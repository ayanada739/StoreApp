using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specifications.Products
{
    public class ProductSpecifications :BaseSpecifications<Product, int>
    {
        public ProductSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();

        }

        public ProductSpecifications(string? sort, int? brandId, int? typeId) : base
            (
               P => 
               (!brandId.HasValue || brandId == P.BrandId)
               &&
               (!typeId.HasValue || typeId == P.TypeId)
            
            )
        {

            //name, priceAsc, priceDwsc
            if(!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {                   
                    case "priceAsc":
                        AddOrderBy( P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending( P => P.Price);
                        break;                   
                    default:
                        AddOrderBy( P => P.Name);
                        break;
                         
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
