namespace BookShopWeb.DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync<TKey>(TKey key);
        Task<IQueryable<T>> GetQueryableAsync();
    }
}
