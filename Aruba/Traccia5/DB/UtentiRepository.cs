using Microsoft.EntityFrameworkCore;

namespace Traccia5.DB
{
    public class UtentiRepository 
    {

        private readonly ArubaDB _db;
        public UtentiRepository(ArubaDB db)
        {
            _db = db;
        }

        public async Task<Utente> Add(Utente item)
        {
            await _db.Set<Utente>().AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task Delete(int id)
        {
            Utente item = await _db.Set<Utente>().FindAsync(id);
            if (item != null)
            {
                _db.Set<Utente>().Remove(item);
                await _db.SaveChangesAsync();
            }
            else throw new KeyNotFoundException($"L'elemento con id:{id} non è stato trovato");
        }

        public async Task<IEnumerable<Utente>> getAll()
        {
            return await _db.Set<Utente>().ToListAsync();
        }


        public async Task<Utente> GetById(int id)
        {
            return await _db.Set<Utente>().FindAsync(id);
        }

        public Task Login()
        {
            throw new NotImplementedException();
        }

        public async Task Update(Utente item)
        {
            _db.Set<Utente>().Update(item);
            await _db.SaveChangesAsync();
        }
    }

}
