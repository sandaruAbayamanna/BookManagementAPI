using BookManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> Books = new List<Book>();

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(Books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            newBook.Id = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
            Books.Add(newBook);
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.ISBN = updatedBook.ISBN;
            book.PublicationDate = updatedBook.PublicationDate;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            Books.Remove(book);
            return NoContent();
        }
    }
}
