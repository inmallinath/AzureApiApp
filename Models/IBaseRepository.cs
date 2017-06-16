using System.Collections.Generic;

namespace AzureApiApp.Models
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetRecords();
        int Count();
        T GetOneRecord(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
    }
}