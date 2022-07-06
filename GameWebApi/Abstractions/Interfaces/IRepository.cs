namespace GameWebApi.Abstractions.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task Update(T updatedObj);
        Task Delete(Guid id);
        Task<IEnumerable<T>> GetAllAsync();

    }
}
