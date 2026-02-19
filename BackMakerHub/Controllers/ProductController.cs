using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackMakerHub.Models;
using BackMakerHub.Services;
using BackMakerHub.DbConnection;
using BackMakerHub.DTO_s;
namespace BackMakerHub.Controllers
{

    [Route("products")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ProductsService _productsService;
        public ProductController(ProductsService productsService)
        {
            _productsService = productsService;
        }
        [HttpGet]
        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            var getAllProducts = _productsService.GetAllProducts();
            return await getAllProducts;
        }
        [HttpPost]
        [Route("addProduct")]
        public async Task<ActionResult<Products>> AddProducts(ProductsCreateDTO addProductDTO)
        {
            try
            {
                var addProduct = await _productsService.AddProductsAsync(addProductDTO);
                return Ok(addProduct);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productsService.RemoveProductAsync(id);
            if (!success)
            {
                return NotFound($"Produit avec {id} introuvable");
            }
                return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ModifyProduct(int id, ModifyProductDTO product)
        {
            var updateProduct = new Products
            { 
                ProductId = product.Id,
                Name = product.ProductName,
                Quantity = product.Quantity,
                Price = product.Price
            };
            var success = await _productsService.ModifyProductAsync(updateProduct);
            if(updateProduct == null)
            {
                return NotFound($"Aucun élément correspondant: {id} introuvable");
            }
            return Ok(updateProduct);
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateQuantityDTO dto)
        {
            var updateQuantityProduct = await _productsService.UpdateProductQuantity(id, dto.Quantity);
            if(updateQuantityProduct == null)
            {
                return NotFound();
            }
            return Ok(updateQuantityProduct);
        }
    }
}
    