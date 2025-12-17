using Store.Domain.Entities.Products;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Spesifications.Products
{
    public class ProductWithBrandTypeSpesifications : BaseSpesifications<int, Product>
    {
        public ProductWithBrandTypeSpesifications(ProductsQueryParams Params) : base
            (
               // Constructor for getting all products
               // Building the criteria based on optional brandId and typeId
               P => (!Params.BrandId.HasValue || P.BrandId == Params.BrandId) &&
                    (!Params.TypeId.HasValue || P.TypeId == Params.TypeId) &&  // Criteria expression
                    (string.IsNullOrEmpty(Params.Search) ||P.Name.ToLower().Contains(Params.Search.ToLower())) // Search functionality
            )
        {
            ApplyPagination(Params.PageIndex,Params.PageSize);
            ApplySorting(Params.Sort);
            AddIncludes();
        }

        public ProductWithBrandTypeSpesifications(int id) : base(P => P.Id == id) // Constructor for getting a product by ID
        {
            AddIncludes();
        }

        private void AddIncludes() // Method to add related entities to include
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }

        private void ApplySorting(string? sort) // Method to apply sorting based on the sort parameter
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        AddOrderByAscending(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderByAscending(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderByAscending(P => P.Name); // Default sorting by Name
            }
        }
    }
}
