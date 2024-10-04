namespace Traccia5.DB
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> getAll();
        Task<T> GetById(int id);
        Task<T> Add(T item);

        Task Update(T item);
        Task Delete(int id);
        Task<string> Login();
    }
}
