using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SearchApi.DbModel;
using SearchApi.Models;

namespace SearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrdersController(IServiceScopeFactory factory)
        {
            _context = factory.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
        }

        // GET: api/Orders
        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        //{
        //    //return await _context.Orders.Include(x => x.Products).Include(x => x.User).ToListAsync();
        //}

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
