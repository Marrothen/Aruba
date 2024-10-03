using Traccia3.Models.DB;

namespace Traccia3.Repository.Interface
{
    public interface IRepository
    {
        Task<IEnumerable<Attivita>> getAll();
        Task<Attivita> GetById(int id);
        Task<Attivita> Add(Attivita item);
        Task Update(Attivita item);
        Task Delete(int id);
    }
}
