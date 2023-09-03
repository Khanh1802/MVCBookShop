using BookShopWeb.DataAccess.Data;
using BookShopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShopWeb.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Category entity)
        {
            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync<TKey>(TKey key)
        {
            return await _context.Categories.FindAsync(key);
        }

        public Task<IQueryable<Category>> GetQueryableAsync()
        {
            return Task.FromResult(_context.Categories.AsQueryable());
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges ();
        }
    }
}
