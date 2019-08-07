using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.EF;
using DAL.Models;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : MyEntity
    {

        private readonly ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<IEnumerable<T>> SelectAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> SelectById(int? id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Insert(T obj)
        {
            _db.Set<T>().Add(obj);
            await Save();
        }

        public async Task Update(T obj)
        {
            var local = _db.Set<T>().Local.FirstOrDefault(f => f.Id == obj.Id); //possible because all model classes inherit MyEntity
            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }
            else
            {
                throw new ArgumentException("Not found");
            }

            _db.Entry(obj).State = EntityState.Modified;
            await Save();
        }

        public async Task Delete(int? id)
        {
            var deleted = await SelectById(id);
            if (deleted != null)
                _db.Set<T>().Remove(deleted);
            else throw new ArgumentException("Not found object id.");
            await Save();
        }

        

        public async Task<IEnumerable<T>> SelectAll(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
