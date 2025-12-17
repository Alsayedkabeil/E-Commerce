using Store.Shared;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Products
{
    // This interface can be expanded with method signatures related to product services.
    public interface IProductServices
    {
        Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductsQueryParams Params);
        Task<ProductResponse> GetProductsByIdAsync(int productId);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
