namespace HotelManagementSystem.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Add(T obj);
        Task<bool> Remove(Guid id);
        Task<bool> Update(T obj);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
    }
}
