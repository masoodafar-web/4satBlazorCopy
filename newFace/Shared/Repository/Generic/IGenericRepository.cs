using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Generic
{
    public interface IGenericRepository<TEntity>
    {
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> FilterQuery(Expression<Func<TEntity, int>> keySelector,
            Expression<Func<TEntity, bool>> predicate, OrderBy orderBy,
          params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetSingleIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        PaginatedList<TEntity> GetAll(int pageIndex, int pageSize);
        PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, OrderBy orderBy = OrderBy.Ascending);
        PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties);
        PaginatedList<TEntity> GetAllKeyList(int pageIndex, int pageSize, List<Expression<Func<TEntity, int>>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById(int id);
        TEntity GetByIdAsNoTracking(int id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> AsQueryable();

        Result Add(TEntity entity);
        Result AddRange(IEnumerable<TEntity> entities);
        Result Update(TEntity entity);
        Result UpdateRange(IEnumerable<TEntity> entities);
        Result Delete(TEntity entity);
        Result DeleteRange(IEnumerable<TEntity> entities);

        void AddWitoutSave(TEntity entity);
        void AddRangeWitoutSave(IEnumerable<TEntity> entities);
        void UpdateWitoutSave(TEntity entity);
        void UpdateRangeWitoutSave(IEnumerable<TEntity> entities);
        void DeleteWitoutSave(TEntity entity);
        void DeleteRangeWitoutSave(IEnumerable<TEntity> entities);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize);
        Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, OrderBy orderBy = OrderBy.Ascending);
        Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetSingleIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

    }
}