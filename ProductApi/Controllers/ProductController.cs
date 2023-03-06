using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProductApi.DbModel;
using ProductApi.DbModels;
using ProductApi.Interface;
using ProductApi.Models;
using RabbitMQ.Client;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        private readonly IMessageProducer _messagePublisher;

        public ProductController(ProductDbContext context, IMessageProducer messagePublisher)
        {
            _messagePublisher = messagePublisher;
            _context = context;
        }

        // GET: api/Product1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct()
        { 
            return await _context.Products.ToListAsync();
        }

        // GET: api/Product1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        {
            
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        //1
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post(string name, int price, string desc)
        {
            ProductDTO product = new ProductDTO()
            {
                Name = name,
                Price = price,
                Description = desc
            };
            
            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
            var json = JsonConvert.SerializeObject(product);
            _messagePublisher.SendMessage(json);
            await Console.Out.WriteLineAsync(json);
            await Console.Out.WriteLineAsync("Я отправил продукт жди его там!");
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
