namespace BookShopWeb.DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync<TKey>(TKey key);
        Task<IQueryable<T>> GetQueryableAsync();
    }
}
