using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared.ErrorModels
{
    public class ValidationErrorResponse
    {

        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest; // 400 Bad Request by default
        public string ErrorMessage { get; set; } = "Validation Error"; // Default error message
        public IEnumerable<ErrorListResponse> Errors { get; set; }
    }
}
