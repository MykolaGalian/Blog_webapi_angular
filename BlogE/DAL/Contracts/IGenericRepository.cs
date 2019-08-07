using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IGenericRepository<T> 
    {
        Task<IEnumerable<T>> SelectAll();
        Task<T> SelectById(int? id);
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(int? id);
        Task Save();
        Task<IEnumerable<T>> SelectAll(Expression<Func<T, bool>> predicate); 
    }
}
