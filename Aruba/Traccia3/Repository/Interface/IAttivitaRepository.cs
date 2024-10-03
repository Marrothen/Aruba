using Traccia3.Models.DB;

namespace Traccia3.Repository.Interface
{
    public interface IAttivitaRepository
    {
        Task<List<Attivita>> getAll();
        Task<Attivita> GetById(string id);
        Task<Attivita> Add(Attivita item);
        Task Update(Attivita item);
        Task Delete(string id);
    }
}
