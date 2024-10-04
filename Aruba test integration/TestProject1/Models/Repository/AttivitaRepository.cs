using Domain.IRepository;
using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;


namespace Domain.Repository
{
    public class AttivitaRepository : IRepository<Attivita>
    {
        
        private readonly ArubaDB _db;
        public AttivitaRepository(ArubaDB db) {
            _db = db;
        }

        public async Task<Attivita> Add(Attivita item)
        {
            await _db.Set<Attivita>().AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task Delete(int id)
        {
            Attivita item=await _db.Set<Attivita>().FindAsync(id);
            if(item != null) {
                _db.Set<Attivita>().Remove(item);
                await _db.SaveChangesAsync();
            }
            else throw new KeyNotFoundException($"L'elemento con id:{id} non è stato trovato");
        }

        public async Task<IEnumerable<Attivita>> getAll()
        {
            return await _db.Set<Attivita>().ToListAsync();
        }


        public async Task<Attivita> GetById(int id)
        {
            return await _db.Set<Attivita>().FindAsync(id);
        }

        public async Task Update(Attivita item)
        {
            _db.Set<Attivita>().Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
