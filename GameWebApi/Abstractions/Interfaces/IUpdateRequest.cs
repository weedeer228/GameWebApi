namespace GameWebApi.Abstractions.Interfaces
{
    public interface IUpdateRequest<T> where T : class
    {
        Task<T> Update(T obj);
    }
}
