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
        // trackChanges parametresi, EF Core'un değişiklikleri izleyip izlemeyeceğini belirttiğimiz parametredir.
        // T ile hangi sınıf türünde döneceği veya hangi sınıf türünde olacağını dinamik olarak belirlenmesi sağlanmıştır. 

        // Veri tabanındaki tüm kayıtları listeleyen asenkron metottur.
        Task<List<T>> FindAllAsync(bool trackChanges);

        // Belirli bir şarta (expression) uyan kayıtları listeleyen asenkron metottur.
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);

        // Yeni bir kayıt eklemek için kullanılan asenkron metottur.
        Task CreateAsync(T entity);

        // Mevcut kaydı güncellemek için kullanılan asenkron metottur.
        Task UpdateAsync(T entity);

        // Mevcut bir kaydı silmek için kullanılan asenkron metottur.
        Task DeleteAsync(T entity);

        // Belirtilen ID'ye sahip tek bir kaydı getiren asenkron metottur.
        Task<T> GetByIdAsync(Guid id, bool trackChanges);

    }
}
