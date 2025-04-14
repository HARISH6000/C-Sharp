using BookApi.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAll()
        {
            _logger.LogInformation("Getting all books");
            return await _bookService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning($"Book with ID {id} not found");
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            var createdBook = await _bookService.CreateAsync(book);
            _logger.LogInformation($"Book created with ID {createdBook.Id}");
            return CreatedAtAction(nameof(Get), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            try
            {
                await _bookService.UpdateAsync(id, book);
                _logger.LogInformation($"Book with ID {id} updated");
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Book with ID {id} not found for update");
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteAsync(id);
                _logger.LogInformation($"Book with ID {id} deleted");
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Book with ID {id} not found for deletion");
                return NotFound();
            }
        }
    }
}