using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    // Represents a product in the store
    public class Product : BaseEntity<int> // Inherits from BaseEntity with an integer Id
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        #region Navigational Property

        public int BrandId { get; set; } // Foreign key to ProductBrand => Convention based
        public ProductBrand Brand { get; set; } // Navigation property to ProductBrand

        public int TypeId { get; set; } // Foreign key to ProductType => Convention based
        public ProductType Type { get; set; } // Navigation property to ProductType

        #endregion

    }
}
