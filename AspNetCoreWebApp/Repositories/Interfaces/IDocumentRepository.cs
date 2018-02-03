using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AspNetCoreWebApp.Repositories.Interfaces
{
    public interface IDocumentRepository<T>
    {
        Task<IList<T>> Find(Expression<Func<T, bool>> query);
        Task Insert(T entity);
        Task Insert(IEnumerable<T> list);
    }

}
