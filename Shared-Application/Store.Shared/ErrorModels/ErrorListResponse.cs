using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.ErrorModels
{
    // Represents a response containing a list of errors
    public class ErrorListResponse
    {
        public string Field { get; set; } // The field associated with the error
        public IEnumerable<string> Errors { get; set; } // List of error messages for the field
    }
}
