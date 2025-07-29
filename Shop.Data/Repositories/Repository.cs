using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Domain.IRepositories;

namespace Shop.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ShopContext _context;
        private DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTrack => Entities.AsNoTracking();

        public Repository(ShopContext context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> conditions = null)
        {
            return conditions == null ? Entities.ToList() : Entities.Where(conditions).ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return Entities.Find(id);
        }

        public virtual TEntity GetByQuery(Expression<Func<TEntity, bool>> conditions)
        {
            return Entities.FirstOrDefault(conditions);
        }

        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                Entities.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual List<TEntity> AddRange(List<TEntity> entities)
        {
            try
            {
                Entities.AddRange(entities);
                _context.SaveChanges();
                return entities;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                Entities.Update(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual List<TEntity> UpdateRange(List<TEntity> entities)
        {
            try
            {
                Entities.UpdateRange(entities);
                _context.SaveChanges();
                return entities;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                Entities.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual bool DeleteRange(List<TEntity> entities)
        {
            try
            {
                Entities.RemoveRange(entities);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool IsExist(Expression<Func<TEntity, bool>> conditions)
        {
            return Entities.Any(conditions);
        }

        #region Async Methods

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> conditions = null)
        {
            return conditions == null ? await Entities.ToListAsync(cancellationToken) : await Entities.Where(conditions).ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await Entities.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity> GetByQueryAsync(Expression<Func<TEntity, bool>> conditions, CancellationToken cancellationToken)
        {
            return await Entities.FirstOrDefaultAsync(conditions, cancellationToken);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await Entities.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                await Entities.AddRangeAsync(entities, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entities;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                Entities.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            {
                Entities.UpdateRange(entities);
                await _context.SaveChangesAsync(cancellationToken);
                return entities;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                Entities.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<bool> DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            try
            { 
                Entities.RemoveRange(entities);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> conditions, CancellationToken cancellationToken)
        {
            return await Entities.AnyAsync(conditions, cancellationToken);
        }

        #endregion
    }
}