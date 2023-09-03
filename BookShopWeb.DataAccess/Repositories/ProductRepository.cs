using BookShopWeb.DataAccess.Data;
using BookShopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShopWeb.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Product entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync<TKey>(TKey key)
        {
            return await _context.Products.FindAsync(key);
        }

        public Task<IQueryable<Product>> GetQueryableAsync()
        {
            return Task.FromResult(_context.Products.AsQueryable());
        }

        public void Update(Product entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
