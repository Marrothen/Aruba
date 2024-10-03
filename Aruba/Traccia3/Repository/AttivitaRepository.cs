using MongoDB.Driver;
using Traccia3.Models.DB;
using Traccia3.Repository.Interface;

namespace Traccia3.Repository
{
    public class AttivitaRepository : IAttivitaRepository
    {
        private readonly MongoDbContext _context;
        public AttivitaRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Attivita> Add(Attivita item)
        {
            await _context.Attivita.InsertOneAsync(item); 
            return item;
        }

        public async Task Delete(string id)
        {
            var deleteResult = await _context.Attivita.DeleteOneAsync(a => a.Id == id); 
            if (deleteResult.DeletedCount == 0)
            {
                throw new KeyNotFoundException($"Elemento con id: {id} non trovato");
            }
        }

        public async Task<List<Attivita>> getAll()
        {
            return await _context.Attivita.Find(_ => true).ToListAsync();
        }

        public async Task<Attivita> GetById(string id)
        {
            var attivita = await _context.Attivita.Find(a => a.Id == id).FirstOrDefaultAsync(); 

            if (attivita == null)
            {
                throw new KeyNotFoundException($"Elemento con id: {id} non trovato");
            }
            return attivita; 
        }

        public async Task Update(Attivita item)
        {
            var updateResult = await _context.Attivita.ReplaceOneAsync(a => a.Id == item.Id, item); 
            if (updateResult.ModifiedCount == 0)
            {
                throw new KeyNotFoundException($"Elemento con id: {item.Id} non trovato");
            }
        }
    }
}
