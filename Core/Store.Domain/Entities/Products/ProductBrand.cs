using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    // Represents a brand of product in the store
    public class ProductBrand : BaseEntity<int> // Inherits from BaseEntity with an integer Id
    {
        public string Name { get; set; }
    }
}
