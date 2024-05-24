using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using web_api_utube.Contracts;

namespace web_api_utube.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _bookService;

        public BooksController(IBooksService booksService)
        {
            _bookService = booksService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _bookService.GetAllBooks();
            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var (book, error) = Book.Create(Guid.NewGuid(), request.Title, request.Description, request.Price);

            if (!string.IsNullOrEmpty(error)) 
            {
                return BadRequest(error);
            }
            await _bookService.CreateBook(book);
            return Ok(book.Id);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBooks(Guid id, [FromBody] BooksRequest request)
        {
            var bookId = await _bookService.UpdateBook(id, request.Title, request.Description, request.Price);
            return Ok(bookId);
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBooks(Guid id, [FromBody] BooksRequest request)
        {
            return Ok(await _bookService.DeleteBook(id));
        }
    }

}
