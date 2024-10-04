using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Traccia5.DB
{

    public class AttivitaRepository : IRepository<Attivita>
    {

        private readonly ArubaDB _db;
        private readonly JwtHelper _jwtHelper;

        public AttivitaRepository(ArubaDB db, IConfiguration config)
        {
            _db = db;
            _jwtHelper = new JwtHelper(config);
        }

        public async Task<Attivita> Add(Attivita item)
        {
            await _db.Set<Attivita>().AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task Delete(int id)
        {
            Attivita item = await _db.Set<Attivita>().FindAsync(id);
            if (item != null)
            {
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

        public async Task<string> Login() {
           return _jwtHelper.GenerateToken("Mario");
        }
    }
}
