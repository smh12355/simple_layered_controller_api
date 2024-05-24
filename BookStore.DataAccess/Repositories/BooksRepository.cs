using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext _context;

        public BooksRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> Get()
        {
            var bookEntities = await _context.Books
                .AsNoTracking()
                .ToListAsync();
            var books = bookEntities
                .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).Book)
                .ToList();
            return books;
        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price
            };
            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            //await _context.Books
            //    .Where(b => b.Id == id)
            //    .ExecuteUpdateAsync(s => s
            //        .SetProperty(b => b.Title, b => title)
            //        .SetProperty(b => b.Description, b => description)
            //        .SetProperty(b => b.Price, b => price));
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            book.Title = title;
            book.Description = description;
            book.Price = price;

            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return id;
        }
    }
}
