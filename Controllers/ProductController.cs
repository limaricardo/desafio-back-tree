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

    }
}
