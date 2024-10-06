using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Lab01_BookStoreOData8.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Formatter;

namespace Lab01_BookStoreOData8.Controllers
{
    public class BooksController : ODataController
    {
        private BookStoreContext _context;

        public BooksController(BookStoreContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
            return Ok(_context.Books);
        }
        [EnableQuery]
        public IActionResult Get(int key, string version)
        {
            return Ok(_context.Books.FirstOrDefault(c => c.Id == key));
        }
        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return Created(book);
        }
        [EnableQuery]
        public IActionResult Put([FromBody]int key, [FromBody] Book update)
        {
            Book existingBook = _context.Books.FirstOrDefault(b => b.Id == key);
            if (existingBook == null) return NotFound();
            // Update the existing book's properties
            existingBook.ISBN = update.ISBN;
            existingBook.Title = update.Title;
            existingBook.Author = update.Author;
            existingBook.Price = update.Price;
            existingBook.Location.Street = update.Location.Street;
            existingBook.Location.City = update.Location.City;
            _context.Books.Update(existingBook);
            _context.SaveChanges();
            return Updated(existingBook);
        }
        [EnableQuery]
        public IActionResult Delete ([FromBody] int key)
        {
            Book b = _context.Books.FirstOrDefault(c => c.Id == key);
            if (b == null)
            {
                return NotFound();
            }
            _context.Books.Remove(b);
            _context.SaveChanges();
            return Ok();
        }
    }
}