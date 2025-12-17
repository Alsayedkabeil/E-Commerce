using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Exceptions
{
    public class ValidationException(IEnumerable<string> errors) : Exception("One or more validation errors occurred.")
    {
        public IEnumerable<string> Errors { get; } = errors; // Property to hold validation errors
    }
}
