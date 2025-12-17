using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Presentation.Attributes;
using Store.Services.Abstractions;
using Store.Shared;
using Store.Shared.Dtos.Products;
using Store.Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation
{
    // API controller for managing products
    // Route: /api/products
    // Inherits from ControllerBase to provide API functionalities
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        #region GetAllProducts
        // Function + Non-Static + Public
        [HttpGet] // GET : baseUrl/api/products?brandId=&typeId=&sort=&search=
                  // Asynchronous action method to get all products
                  // Returns IActionResult to represent HTTP responses
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ValidationErrorResponse))]
        [Cache(200)]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductsQueryParams Params) // [FromQuery] to bind query parameters
        {
            var result = await _serviceManager.ProductServices.GetAllProductsAsync(Params);
            if (result is null)
            {
                return BadRequest(); // Return 400 Bad Request if no products found
            }
            return Ok(result); // Return 200 OK with the list of products
        }
        #endregion

        #region GetProductById
        // Function + Non-Static + Public
        // GET : baseUrl/api/products/{id}
        // Asynchronous action method to get a product by its ID
        // Returns IActionResult to represent HTTP responses
        [HttpGet("{id}")]  // Route parameter for product ID
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResponse>> GetProductById(int? id)
        {
            if (id is null || id <= 0)
            {
                return BadRequest(); // Return 400 Bad Request if ID is null or invalid
            }
            var result = await _serviceManager.ProductServices.GetProductsByIdAsync(id.Value);
           // if (result is null)
            //{
             //   return NotFound(); // Return 404 Not Found if product with the specified ID does not exist
           // }
            return Ok(result); // Return 200 OK with the product details
        }

        #endregion

        #region GetAllBrandsAsync
        // Function + Non-Static + Public
        // GET : baseUrl/api/products/brands
        // Asynchronous action method to get all product brands
        // Returns IActionResult to represent HTTP responses
        [HttpGet("brands")] // GET : baseUrl/api/products
                            // Asynchronous action method to get all products
                            // Returns IActionResult to represent HTTP responses
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        public async Task<ActionResult<BrandTypeResponse>> GetAllBrands()
        {
            var result = await _serviceManager.ProductServices.GetAllBrandsAsync();
            if (result is null)
            {
                return BadRequest(); // Return 400 Bad Request if no products found
            }
            return Ok(result); // Return 200 OK with the list of products
        }
        #endregion

        #region GetAllTypesAsync
        // Function + Non-Static + Public
        // GET : baseUrl/api/products/brands
        // Asynchronous action method to get all product brands
        // Returns IActionResult to represent HTTP responses
        [HttpGet("types")] // GET : baseUrl/api/products
                           // Asynchronous action method to get all products
                           // Returns IActionResult to represent HTTP responses
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        public async Task<ActionResult<BrandTypeResponse>> GetAllTypes()
        {
            var result = await _serviceManager.ProductServices.GetAllTypesAsync();
            if (result is null)
            {
                return BadRequest(); // Return 400 Bad Request if no products found
            }
            return Ok(result); // Return 200 OK with the list of products
        }
        #endregion
    }
}
