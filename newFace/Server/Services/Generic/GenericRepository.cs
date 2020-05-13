using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using newFace.Server.Data;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace newFace.Server.Services.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private newFaceDbContext _context;
        private DbSet<TEntity> _dbset;
        private bool _disposed;

        public GenericRepository(newFaceDbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }

        public int Count()
        {
            return _dbset.Count();

        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Count(predicate);

        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.FirstOrDefault(predicate);

        }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Any(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbset;

        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize)
        {
            return GetAll(pageIndex, pageSize, x => x.Id);
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return GetAll(pageIndex, pageSize, keySelector, null, orderBy);
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = FilterQuery(keySelector, predicate, orderBy, includeProperties);
            var total = entities.Count();// entities.Count() is different than pageSize
            entities = entities.Paginate(pageIndex, pageSize);
            return entities.ToPaginatedList(pageIndex, pageSize, total);
        }

        public PaginatedList<TEntity> GetAllKeyList(int pageIndex, int pageSize, List<Expression<Func<TEntity, int>>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = FilterQueryKeyList(keySelector, predicate, orderBy, includeProperties);
            var total = entities.Count();// entities.Count() is different than pageSize
            entities = entities.Paginate(pageIndex, pageSize);
            return entities.ToPaginatedList(pageIndex, pageSize, total);
        }

        public IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            return (predicate != null) ? entities.Where(predicate) : entities;
        }

        public TEntity GetById(int id)
        {
            return _dbset.Find(id);
        }
        public TEntity GetByIdAsNoTracking(int id)
        {
            return _dbset.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }
        public TEntity GetSingleIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            return entities.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }
        public IQueryable<TEntity> AsQueryable()
        {
            return _dbset.AsQueryable();
        }

        public Result Add(TEntity entity)
        {
            Result Result = new Result();
            try
            {
                _dbset.Add(entity);
                if (Convert.ToBoolean(_context.SaveChanges()))
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "عملیات افزودن با موفقیت انجام شد";
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "متاسفانه عملیات افزودن با موفقیت انجام نشد";
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = ex.Message;
                return Result;
            }
        }
        public Result AddRange(IEnumerable<TEntity> entities)
        {
            Result Result = new Result();
            try
            {
                _dbset.AddRange(entities);
                if (Convert.ToBoolean(_context.SaveChanges()))
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "عملیات افزودن با موفقیت انجام شد";
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "متاسفانه عملیات افزودن با موفقیت انجام نشد";
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = ex.Message;
                return Result;
            }
        }
        public Result Update(TEntity entity)
        {
            Result Result = new Result();
            try
            {

                _dbset.Update(entity);
                if (Convert.ToBoolean(_context.SaveChanges()))
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "عملیات ویرایش با موفقیت انجام شد";
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "متاسفانه عملیات ویرایش با موفقیت انجام نشد";
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = ex.Message;
                return Result;
            }
        }
        public Result UpdateRange(IEnumerable<TEntity> entities)
        {
            Result Result = new Result();
            try
            {
                _dbset.UpdateRange(entities);
                if (Convert.ToBoolean(_context.SaveChanges()))
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "عملیات ویرایش با موفقیت انجام شد";
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "متاسفانه عملیات ویرایش با موفقیت انجام نشد";
                    return Result;
                }

            }
            catch (Exception ex)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = ex.Message;
                return Result;
            }
        }
        public Result Delete(TEntity entity)
        {
            Result Result = new Result();
            try
            {
                _dbset.Remove(entity);
                if (Convert.ToBoolean(_context.SaveChanges()))
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "عملیات حذف با موفقیت انجام شد";
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "متاسفانه عملیات حذف با موفقیت انجام نشد";
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = ex.Message;
                return Result;
            }
        }
        public Result DeleteRange(IEnumerable<TEntity> entity)
        {
            Result Result = new Result();
            try
            {
                _dbset.RemoveRange(entity);
                if (Convert.ToBoolean(_context.SaveChanges()))
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "عملیات حذف با موفقیت انجام شد";
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "متاسفانه عملیات حذف با موفقیت انجام نشد";
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = ex.Message;
                return Result;
            }
        }

        public void AddWitoutSave(TEntity entity)
        {
            _dbset.Add(entity);
        }
        public void AddRangeWitoutSave(IEnumerable<TEntity> entities)
        {
            _dbset.AddRange(entities);
        }
        public void UpdateWitoutSave(TEntity entity)
        {
            _dbset.Update(entity);
        }
        public void UpdateRangeWitoutSave(IEnumerable<TEntity> entities)
        {
            _dbset.UpdateRange(entities);
        }
        public void DeleteWitoutSave(TEntity entity)
        {
            _dbset.Remove(entity);
        }
        public void DeleteRangeWitoutSave(IEnumerable<TEntity> entity)
        {
            _dbset.RemoveRange(entity);
        }

        public IQueryable<TEntity> FilterQuery(Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            entities = (predicate != null) ? entities.Where(predicate) : entities;
            if (keySelector != null)
            {

                entities = (orderBy == OrderBy.Ascending)
                    ? entities.OrderBy(keySelector)
                    : entities.OrderByDescending(keySelector);


            }
            return entities;
        }

        public IQueryable<TEntity> FilterQueryKeyList(List<Expression<Func<TEntity, int>>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = IncludeProperties(includeProperties);
            entities = (predicate != null) ? entities.Where(predicate) : entities;
            if (keySelector.Any())
            {
                if (keySelector.Count() == 1)
                {
                    entities = (orderBy == OrderBy.Ascending)
                        ? entities.OrderBy(keySelector[0])
                        : entities.OrderByDescending(keySelector[0]);
                }
                else if (keySelector.Count() > 1)
                {
                    var flag = true;
                    foreach (var item in keySelector)
                    {
                        if (flag)
                        {
                            entities = (orderBy == OrderBy.Ascending)
                                ? entities.OrderBy(item)
                                : entities.OrderByDescending(item);
                            flag = false;
                        }
                        else
                        {
                            entities = (orderBy == OrderBy.Ascending)
                                ? entities.OrderBy(item)
                                : entities.OrderByDescending(item);
                        }

                    }
                }



            }
            return entities;
        }


        public Task<List<TEntity>> GetAllAsync()
        {
            return _dbset.ToListAsync();
        }
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.CountAsync(predicate);

        }
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.FirstOrDefaultAsync(predicate);

        }
        public Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize)
        {
            return GetAllAsync(pageIndex, pageSize, x => x.Id);
        }

        public Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return GetAllAsync(pageIndex, pageSize, keySelector, null, orderBy);
        }

        public async Task<PaginatedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = FilterQuery(keySelector, predicate, orderBy, includeProperties);
            var total = await entities.CountAsync();// entities.CountAsync() is different than pageSize
            entities = entities.Paginate(pageIndex, pageSize);
            var list = await entities.ToListAsync();
            return list.ToPaginatedList(pageIndex, pageSize, total);
        }

        public Task<List<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            return entities.ToListAsync();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return _dbset.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<TEntity> GetSingleIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            return entities.FirstOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate).ToListAsync();
        }

        public Task AddAsync(TEntity entity)
        {
            _dbset.Add(entity);
            return _context.SaveChangesAsync();
        }
        public Task UpdateAsync(TEntity entity)
        {
            _dbset.Update(entity);
            return _context.SaveChangesAsync();
        }
        public Task DeleteAsync(TEntity entity)
        {
            _dbset.Remove(entity);
            return _context.SaveChangesAsync();
        }


        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _dbset;
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}