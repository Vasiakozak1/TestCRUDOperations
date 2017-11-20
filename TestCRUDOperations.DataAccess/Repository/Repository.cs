using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCRUDOperations.DataAccess.Context;
namespace TestCRUDOperations.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>()
                .Add(entity);
        }

        public void Delete(int id)
        {
            var element = _context.Set<T>()
                .Find(id);

            if (element != null)
                _context.Set<T>()
                    .Remove(element);
        }

        public T Get(int id)
        {
            return _context.Set<T>()
                .Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            var element = _context.Set<T>()
                .Find(entity.Id);
            if (element != null)
                _context.Set<T>()
                    .Update(entity);
        }
    }
}
