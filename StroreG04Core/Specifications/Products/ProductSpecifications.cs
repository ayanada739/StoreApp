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
        

        public ProductSpecifications(ProductSpecParams productSpec) : base
            (
               P => 
               (!productSpec.BrandId.HasValue || productSpec.BrandId == P.BrandId)
               &&
               (!productSpec.TypeId.HasValue || productSpec.TypeId == P.TypeId)
            
            )
        {

            //name, priceAsc, priceDwsc
            if(!string.IsNullOrEmpty(productSpec.sort))
            {
                switch(productSpec.sort)
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

            ApplyPagination(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize) ;  
        }

        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
