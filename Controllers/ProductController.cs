using AutoMapper;
using DesafioBackTree.Dto;
using DesafioBackTree.Interfaces;
using DesafioBackTree.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackTree.Controllers
{
    [Route("api/[controller]")]
    public class ProductController: Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Product>))]
        public IActionResult GetProduct()
        {
            var products = _productRepository.GetProducts();

            if(!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody] ProductDto productCreate) 
        {
            if(productCreate == null) 
                return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                                            .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper())
                                            .FirstOrDefault();

            if(product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);

            if(!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(int productId, [FromBody]ProductDto updatedProduct)
        {
            if(updatedProduct == null)
            {
                return BadRequest(ModelState);
            }

            if(productId != updatedProduct.Id)
            {
                return BadRequest(ModelState);
            }

            if(!_productRepository.ProductExists(productId))
            {
                return NotFound();
            } 

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productMap = _mapper.Map<Product>(updatedProduct);

            if(!_productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int productId)
        {
            if(!_productRepository.ProductExists(productId))
            {
                return NotFound();
            } 

            var productToDelete = _productRepository.GetProduct(productId);
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Product");
            }

            return NoContent();
        }
    }
}
