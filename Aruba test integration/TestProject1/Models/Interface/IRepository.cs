using Domain.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> getAll();
        Task<T> GetById(int id);
        Task<T> Add(T item);

        Task Update(T item);
        Task Delete(int id);
    }
}
