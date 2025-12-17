using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Domain.Exceptions;
using Store.Services.Abstractions.Products;
using Store.Services.Spesifications;
using Store.Services.Spesifications.Products;
using Store.Shared;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Products
{
    public class ProductServices(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductServices
    {
        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductsQueryParams Params)
        {
            //var spec = new BaseSpesifications<int, Product>(null);
            //spec.Includes.Add(p => p.Brand);
            //spec.Includes.Add(p => p.Type);
            var spec = new ProductWithBrandTypeSpesifications(Params);
            var products = await _unitOfWork.GetRepostory<int, Product>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);
            var countSpec = new ProductCountWithoutSpesification(Params);
            var RealCount = await _unitOfWork.GetRepostory<int, Product>().GetCountWithoutPaginationAsync(countSpec);
            return new PaginationResponse<ProductResponse>(Params.PageIndex, Params.PageSize, RealCount, result);
        }
        public async Task<ProductResponse> GetProductsByIdAsync(int productId)
        {
            //var spec = new BaseSpesifications<int, Product>(P => P.Id == productId);
            var spec = new ProductWithBrandTypeSpesifications(productId);
            var product = await _unitOfWork.GetRepostory<int, Product>().GetAsync(spec);
            if (product is null)
            {
                throw new ProductNotFoundException(productId);
            }
            var result = _mapper.Map<ProductResponse>(product);
            return result;
        }
        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var spec = new BaseSpesifications<int, ProductBrand>(null);
            var brands = await _unitOfWork.GetRepostory<int, ProductBrand>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var spec = new BaseSpesifications<int, ProductType>(null);
            var types = await _unitOfWork.GetRepostory<int, ProductType>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
            return result;
        }
    }
}
