using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public interface IBookstoreRepository<TEntity>
    {
        IList<TEntity> List();

        TEntity find(int id);
        void add(TEntity entity);
        void update(int id, TEntity entity);
        void delete(int id);

        List<TEntity> search(string term);
    }
}
