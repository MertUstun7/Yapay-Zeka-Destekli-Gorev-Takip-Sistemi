using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IGTSBase<T>
    {
        // Aşağıda servislerde kullanılacak olan temel fonksiyonların tanımı yapılmıştır.
        Task<List<T>> FindAllAsync(bool trackChanges);
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
