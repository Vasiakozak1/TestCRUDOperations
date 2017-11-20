using System.Collections.Generic;
namespace TestCRUDOperations.DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity: class, IEntity
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}