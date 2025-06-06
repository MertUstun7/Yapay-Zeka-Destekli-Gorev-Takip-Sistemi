using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public abstract class GTSBase<T> : IGTSBase<T> where T :class
    {
        //Context işlemi yapıldı burada veri tabanına erişildi. gtsbase ile işlem interfacelere erişim yapıldı.
        protected readonly GTSDbContext _context;
        public GTSBase (GTSDbContext context)
        {
            _context = context;
        }

        //İlgili tablonun (T) tüm verilerini getirir.
        public async Task<List<T>> FindAllAsync(bool trackChanges)
        {
            return await (trackChanges
                ? _context.Set<T>()
                : _context.Set<T>().AsNoTracking())
                .ToListAsync();
        }

        // Tanımlanan koşula göre ilgili tablodaki (T) verileri getirir.
        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            IQueryable<T> query = _context.Set<T>().Where(expression);
            if (!trackChanges)
                query = query.AsNoTracking();
            return await query.ToListAsync();
        }
        // İlgili tablo üzerinden (T) Id üzerinden sorgulama yapar.
        public async Task<T> GetByIdAsync(Guid id, bool trackChanges)
        {
            IQueryable<T> query = _context.Set<T>();
            if (!trackChanges)
                query = query.AsNoTracking();
            return await query.SingleOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id)
                ?? throw new ArgumentException($"Entity with ID {id} not found.");
        }

        // Veri tabanına gönderilen entity'i ilgili tabloya (T) veriyi ekler
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);
        }

        // GÖnderilen entity nesnesini ilgili tabloda (T) günceller
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Update(entity);
        }
        // İlgili tabloda yer alan (T) entity nesnesini siler.
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Remove(entity);
        }
    }
}
