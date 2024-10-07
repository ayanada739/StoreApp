using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specifications.Products
{
    public class ProductWithCountSpecifications:BaseSpecifications<Product, int>
    {
        public ProductWithCountSpecifications(ProductSpecParams productSpec) 
            : base
            (
               P =>
               (!productSpec.BrandId.HasValue || productSpec.BrandId == P.BrandId)
               &&
               (!productSpec.TypeId.HasValue || productSpec.TypeId == P.TypeId)

            )
        {


        }

            
    }
}
