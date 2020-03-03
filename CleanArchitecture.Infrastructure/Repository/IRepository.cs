using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repository
{
    /// <summary>
    /// Gemeric repository interface definition.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Find entity with unique id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <param name="includeProperties">if entity has fk relation load with main entity.</param>
        /// <returns>Finded entity object.</returns>
        TEntity FindById(long id, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Find method with fk.
        /// </summary>
        /// <param name="includeProperties">if entity has fk relation load with main entity.</param>
        /// <returns>Finded entity queryable list.</returns>
        IQueryable<TEntity> FindAll(params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Find method with predicate and fk.
        /// </summary>
        /// <param name="predicate">Condition expression.</param>
        /// <param name="includeProperties">if entity has fk relation load with main entity.</param>
        /// <returns>Finded entity queryable list.</returns>
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Update entiy action.
        /// </summary>
        /// <param name="entity">Entity object which will be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        /// UpdateBatch
        /// </summary>
        /// <param name="updateProperty">updateProperty</param>
        /// <param name="predicate">predicate</param>
        void UpdateAll(Expression<Func<TEntity, object>> updateProperty, object updateValue, Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Add Action
        /// </summary>
        /// <param name="entity">Added entity object</param>
        void Add(TEntity entity);

        /// <summary>
        /// Add collection of entities
        /// </summary>
        /// <param name="entities">Entity collection</param>
        void AddBatch(ICollection<TEntity> entities);

        /// <summary>
        /// Remove with id action. Actually it means only record update.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Removed entity object.</returns>
        TEntity Remove(long id);

        /// <summary>
        /// Remove collection of entities   
        /// </summary>
        /// <param name="entities">Entity collection</param>
        void RemoveBatch(ICollection<TEntity> entities);

        /// <summary>
        /// Remove with entity.Actually it means only record update.
        /// </summary>
        /// <param name="entity">Removed entity object.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Actual removing operation for ICollection
        /// </summary>
        /// <param name="entities"></param>
        void RemoveHardBatch(ICollection<TEntity> entities);

        /// <summary>
        /// Actual removing operation for ICollection
        /// </summary>
        /// <param name="entities"></param>
        void RemoveHard(TEntity entity);


        int ExecuteSqlCommand(string sql, params object[] parameters);

        IQueryable<TEntity> FindAll(int page, int pageSize, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> FindAll(int page, int pageSize, Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FromSql(string sqlQuery, params object[] parameters);
    }
}
