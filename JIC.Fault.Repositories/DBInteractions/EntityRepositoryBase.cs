using System;
using System.Collections.Generic;
using System.Data.Entity;
//using EntityFramework.Extensions;
//using EntityFramework.BulkInsert.Extensions;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using JIC.Fault.Repositories;
using JIC.Fault.Repositories.DBInteractions;
using System.Data.Entity.Validation;

namespace JIC.Repositories.DBInteractions
{
    public abstract class EntityRepositoryBase<EntityClassType>
        where EntityClassType : class
    {
        #region Variables

        readonly IDbSet<EntityClassType> dbset;

        #endregion

        #region Properties

        protected JICFaultContext DataContext { get { return DBFactory.Get(); } }

        #endregion

        #region Contructors

        public EntityRepositoryBase()
        {
            this.dbset = DataContext.Set<EntityClassType>();
        }

        #endregion

        #region Methods

        public virtual void Add(EntityClassType entity)
        {
            dynamic _entity = entity;
            if (entity.GetType().GetProperty("CreatedAt") != null)
                _entity.CreatedAt = DateTime.Now;
         
            if (entity.GetType().GetProperty("CreatedBy") != null)
                _entity.CreatedBy = _entity.CreatedBy ?? Utitlities.CurrentUsername;
          

            dbset.Add(entity);
        }

        public virtual void Add(List<EntityClassType> entityList)
        {
            entityList.ForEach(entity =>
            {
                dynamic _entity = entity;
                if (entity.GetType().GetProperty("CreatedAt") != null)
                    _entity.CreatedAt = DateTime.Now;
                if (entity.GetType().GetProperty("CreatedBy") != null)
                    _entity.CreatedBy = _entity.CreatedBy ?? Utitlities.CurrentUsername;
            });

            //DataContext.BulkInsert(entityList, new BulkInsertOptions() { EnableStreaming = true, TimeOut = 5 });
        }

        #region For Deleting if rong
        public EntityClassType GetFirsrOrDefualt(Expression<Func<EntityClassType, bool>> predicate) 
        {
            var query = DataContext.Set<EntityClassType>();

            //if (!string.IsNullOrWhiteSpace(includePath))
            //{
            //    return query.Include(includePath);
            //}

            return query.FirstOrDefault(predicate);
        }
        public EntityClassType GetWithInclude(Expression<Func<EntityClassType, bool>> predicate, params Expression<Func<EntityClassType, object>>[] includes)
        {
            DbQuery<EntityClassType> query = privatInclude(includes);

            return query.Single(predicate);
        }
        
        private DbQuery<EntityClassType> privatInclude(Expression<Func<EntityClassType, object>>[] includes)
        {
            List<string> includelist = new List<string>();

            foreach (var item in includes)
            {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }
            // var query = Context.Set<TEntity>();
            var entity = DataContext.Set<EntityClassType>();
            DbQuery<EntityClassType> query = entity;
            foreach (var item in includelist)
            {
                query = query.Include(item);
            }

            // includelist.ForEach(x => query = query.Include(x));
            return query;
        }

        #endregion
        public virtual void AddtoContext(EntityClassType entity)
        {
            dbset.Add(entity);
            DataContext.Entry<EntityClassType>(entity).State = EntityState.Unchanged;
        }

        public virtual void RemoveFromContext(EntityClassType entity)
        {
            dbset.Local.Clear();
            DataContext.Entry<EntityClassType>(entity).State = EntityState.Detached;
        }

        public virtual void Update(EntityClassType entity)
        {
            dynamic _entity = entity;
            if (entity.GetType().GetProperty("LastModifiedAt") != null)
                _entity.LastModifiedAt = DateTime.Now;

            if (entity.GetType().GetProperty("LastModifiedBy") != null)
                _entity.LastModifiedBy = Utitlities.CurrentUsername;

            DataContext.Entry<EntityClassType>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(EntityClassType entity)
        {
            DataContext.Entry<EntityClassType>(entity);
            DataContext.Entry<EntityClassType>(entity).State = EntityState.Deleted;
        }

        public virtual EntityClassType GetByID(long ID)
        {
            return dbset.Find(ID);
        }

        public virtual EntityClassType GetByID(int ID)
        {
            return dbset.Find(ID);
        }

        public virtual EntityClassType GetByID(short ID)
        {
            return dbset.Find(ID);
        }

        public virtual EntityClassType GetByID(Guid ID)
        {
            return dbset.Find(ID);
        }

        public virtual IEnumerable<EntityClassType> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IQueryable<EntityClassType> GetAllQuery()
        {
            return dbset;
        }

        public void Save()
        {
            //DataContext.SaveChanges();
            try
            {
                DataContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                e = null;
              //  throw newException;
            }
        }


        #endregion
    }
}
