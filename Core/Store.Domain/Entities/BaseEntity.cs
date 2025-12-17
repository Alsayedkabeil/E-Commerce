using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    // A generic base entity class with an Id property of type TKey
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } // Unique identifier for the entity
    }
}
