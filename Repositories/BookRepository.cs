using Microsoft.EntityFrameworkCore;
using BookshelfApi.Data;
using BookshelfApi.Interfaces;
using BookshelfApi.Models;

namespace BookshelfApi.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetByAuthorAsync(string author)
        {
            return await _context.Books.Where(a  => a.Author == author).ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> CreateAsync(Book entity)
        {
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Book entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
