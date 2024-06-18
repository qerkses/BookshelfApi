using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookshelfApi.Data;
using BookshelfApi.Models;
using BookshelfApi.Interfaces;


namespace BookshelfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;

        public BooksController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }


        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetAllAsync();
        }

        // GET: api/<BooksController>
        [HttpGet("author/{author}")]
        public async Task<IEnumerable<Book>> GetBooksByAuthor(string author)
        {
            return await _bookRepository.GetByAuthorAsync(author);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            // Return 404 Not Found if the book with the specified ID is not found
            if (book == null)
            {
                return NotFound(); 
            }

            return book;
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            // Ensure IsRead is set to false if not provided
            if (!book.IsRead)
            {
                book.IsRead = false;
            }

            if (!ModelState.IsValid)
            {
                // Return 400 Bad Request with validation error details
                return BadRequest(ModelState);
            }

            return await _bookRepository.CreateAsync(book);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                // Return 400 Bad Request with validation error details
                return BadRequest(ModelState);
            }

            var tempBook = await _bookRepository.GetByIdAsync(id);

            // Return 404 Not Found if the book with the specified ID is not found
            if (tempBook == null)
            {
                return NotFound();
            }

            book.Id = id;
            await _bookRepository.UpdateAsync(book);
            return NoContent();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _bookRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
