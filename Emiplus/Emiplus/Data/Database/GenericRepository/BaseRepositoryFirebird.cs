using Emiplus.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emiplus.Data.GenericRepository
{
    public class BaseRepositoryFirebird<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private DataFirebird _context;

        public BaseRepositoryFirebird(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _context = unitOfWork as DataFirebird;
        }

        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> List()
        {
            return _context.Set<T>();
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public void Edit(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
