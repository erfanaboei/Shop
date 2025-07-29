using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> conditions = null);
        TEntity GetById(int id);
        TEntity GetByQuery(Expression<Func<TEntity, bool>> conditions);
        TEntity Add(TEntity entity);
        List<TEntity> AddRange(List<TEntity> entities);
        TEntity Update(TEntity entity);
        List<TEntity> UpdateRange(List<TEntity> entities);
        bool Delete(TEntity entity);
        bool DeleteRange(List<TEntity> entities);
        bool IsExist(Expression<Func<TEntity, bool>> conditions);

        #region AsyncMethods

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> conditions = null);
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TEntity> GetByQueryAsync(Expression<Func<TEntity, bool>> conditions, CancellationToken cancellationToken);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<List<TEntity>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> conditions, CancellationToken cancellationToken);

        #endregion
    }
}