using Lab01_BookStoreOData8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Lab01_BookStoreOData8.Controllers
{
    public class PressesController : ODataController
    {
        private BookStoreContext _context;

        public PressesController (BookStoreContext context)
        {
            _context = context;
            if (context.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Presses.Add(b.Press);
                } 
                context.SaveChanges();
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Presses);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] Press press)
        {
            _context.Presses.Add(press);
            _context.SaveChanges();
            return Created(press);
        }
    }
}
