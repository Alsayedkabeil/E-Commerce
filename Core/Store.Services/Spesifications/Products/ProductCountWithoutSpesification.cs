using Store.Domain.Entities.Products;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Spesifications.Products
{
    public class ProductCountWithoutSpesification : BaseSpesifications<int,Product>
    {
        public ProductCountWithoutSpesification(ProductsQueryParams Params) : base
            (
               // Constructor for counting products without pagination
               // Building the criteria based on optional brandId and typeId
               P => (!Params.BrandId.HasValue || P.BrandId == Params.BrandId) &&
                    (!Params.TypeId.HasValue || P.TypeId == Params.TypeId) &&  // Criteria expression
                    (string.IsNullOrEmpty(Params.Search) || P.Name.ToLower().Contains(Params.Search.ToLower())) // Search functionality
            )
        {

        }
    }
}
