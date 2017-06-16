using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureApiApp.Models
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApiDbContext _context;

        public BaseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public void Delete(T entity)
        {
            //throw new NotImplementedException();
        }

        public T GetOneRecord(int id)
        {
            return _context.Set<T>().FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<T> GetRecords()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public void Update(T entity)
        {
            //throw new NotImplementedException();
        }
    }
}