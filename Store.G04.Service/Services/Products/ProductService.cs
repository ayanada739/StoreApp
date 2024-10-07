using AutoMapper;
using Store.G04.Core;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using Store.G04.Core.Sevices.Contract;
using Store.G04.Core.Specifications;
using Store.G04.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var spec = new ProductSpecifications() ;
            return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec));

        }


        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec);
            var mappedProduct = _mapper.Map<ProductDto>(product);
            return mappedProduct;
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            var mappedBrand = _mapper.Map<IEnumerable<TypeBrandDto>>(brands);
            return mappedBrand;
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
           return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<Product, int>().GetAllAsync());

        }



   
    }
}
