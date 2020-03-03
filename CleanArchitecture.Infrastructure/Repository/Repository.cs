using CleanArchitecture.Core.Entities;
using CleanArchitecture.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repository
{
    /// <summary>
    /// Gemeric repository class definition.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly AppDbContext context;
        private DbSet<TEntity> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// Default constructor.
        /// </summary>
        /// <param name="context">.net entity framework db conext interface</param>
        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// Default constructor.
        /// </summary>
        /// <param name="context">.net entity framework db conext interface</param>
        //public Repository(TDbContext context, AppDbContext appDbContext)
        //{
        //    this.context = context;
        //    this.appDbContext = appDbContext;
        //}

        /// <summary>
        /// Add Action
        /// </summary>
        /// <param name="entity">Added entity object</param>
        public virtual void Add(TEntity entity)
        {
            var table = context.Set<TEntity>();
            //EncryptFieldsIfAny(entity);
            table.Add(entity);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds collection of entities
        /// </summary>
        /// <param name="entities">Entity collection</param>
        public virtual void AddBatch(ICollection<TEntity> entities)
        {
            var table = context.Set<TEntity>();
            foreach (var entity in entities)
                //EncryptFieldsIfAny(entity);
                table.Add(entity);

            context.SaveChanges();
        }

        public virtual IQueryable<TEntity> FindAllIncludeDeleted(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = context.Set<TEntity>();
            //foreach (var entity in items)
            //{
            //    DecryptFieldsIfAny(entity);
            //}
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                    items = items.Include(includeProperty);

            return items;
        }

        public IQueryable<TEntity> FindAllIncludeDeleted(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var table = FindAllIncludeDeleted(includeProperties);
            //foreach (var entity in table)
            //{
            //    DecryptFieldsIfAny(entity);
            //}
            return table.Where(predicate);
        }

        /// <summary>
        /// Find method with fk.
        /// </summary>
        /// <param name="includeProperties">if entity has fk relation load with main entity.</param>
        /// <returns>Finded entity queryable list.</returns>
        public virtual IQueryable<TEntity> FindAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = context.Set<TEntity>().Where(x => x.IsDeleted == false);
            //foreach (var entity in items)
            //{
            //    DecryptFieldsIfAny(entity);
            //}
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                    items = items.Include(includeProperty);

            return items;
        }

        /// <summary>
        /// Find method with predicate and fk.
        /// </summary>
        /// <param name="predicate">Condition expression.</param>
        /// <param name="includeProperties">if entity has fk relation load with main entity.</param>
        /// <returns>Finded entity queryable list.</returns>
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var table = FindAll(includeProperties);
            //foreach (var entity in table)
            //{
            //    DecryptFieldsIfAny(entity);
            //}
            return table.Where(predicate);
        }

        /// <summary>
        /// Find entity with unique id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <param name="includeProperties">if entity has fk relation load with main entity.</param>
        /// <returns>Finded entity object.</returns>
        public virtual TEntity FindById(long id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(s => s.Id.Equals(id));
        }

        /// <summary>
        /// Remove with id action. Actually it means only record update.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Removed entity object.</returns>
        public virtual TEntity Remove(long id)
        {
            var entity = FindById(id);
            Remove(entity);
            return entity;
        }

        /// <summary>
        /// Adds collection of entities
        /// </summary>
        /// <param name="entities">Entity collection</param>
        public virtual void RemoveBatch(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
                entity.IsDeleted = true;

            context.SaveChanges();
        }

        /// <summary>
        /// Remove with entity.Actually it means only record update.
        /// </summary>
        /// <param name="entity">Removed entity object.</param>
        public virtual void Remove(TEntity entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        /// <summary>
        /// Update entiy action.
        /// </summary>
        /// <param name="entity">Entity object which will be updated.</param>
        public virtual void Update(TEntity entity)
        {
            var table = context.Set<TEntity>();
            table.Update(entity);
            context.SaveChanges();
        }


        public virtual void RemoveHardBatch(ICollection<TEntity> entities)
        {
            var table = context.Set<TEntity>();
            table.RemoveRange(entities);
            context.SaveChanges();

        }

        public virtual void RemoveHard(TEntity entity)
        {
            var table = context.Set<TEntity>();
            table.Remove(entity);
            context.SaveChanges();


        }

        /// <summary>
        /// Update collection of entities
        /// </summary>
        /// <param name="updateProperty">updateProperty</param>
        /// <param name="updateValue">updateValue</param>
        /// <param name="predicate">predicate</param>
        public virtual void UpdateAll(Expression<Func<TEntity, object>> updateProperty, object updateValue, Expression<Func<TEntity, bool>> predicate)
        {
            var unaryExpression = updateProperty.Body as UnaryExpression;
            var updateType = unaryExpression.Operand.Type;
        }

        /// <summary>
        /// Execute sql statement on db.
        /// </summary>
        /// <param name="sqlQuery">SQL query.</param>
        /// <param name="parameters">SQL query paramters.</param>
        /// <returns>Finded entity queryable list.</returns>
        public IQueryable<TEntity> FromSql(string sqlQuery, params object[] parameters)
        {
            return context.Set<TEntity>().FromSql(sqlQuery, parameters);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return context.ExecuteSqlCommand(sql, parameters);
        }

        public IQueryable<TEntity> GetItems()
        {
            IQueryable<TEntity> items = context.Set<TEntity>();
            return items;
        }

        public IQueryable<TEntity> FindAll(int page, int pageSize, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            page = page <= 0 ? 0 : page - 1;
            return FindAll(predicate, includeProperties).Skip(page * pageSize).Take(pageSize);
        }

        public IQueryable<TEntity> FindAll(int page, int pageSize, Expression<Func<TEntity, bool>> predicate)
        {

            page = page <= 0 ? 0 : page - 1;
            return FindAll(predicate).Skip(page * pageSize).Take(pageSize);
        }
    }
}
