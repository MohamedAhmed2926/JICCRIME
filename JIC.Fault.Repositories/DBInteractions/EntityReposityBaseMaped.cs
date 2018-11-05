using JIC.Base.Interfaces;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Fault.Repositories;

namespace JIC.Repositories.DBInteractions
{
    public abstract class EntityRepositoryBase<RepoType, ViewType,IdType> : EntityRepositoryBase<RepoType>, IRepositoryBase<ViewType, IdType>
        where RepoType : class
        where ViewType : class
    {
        public IMapper Mapper { get; }

        public EntityRepositoryBase()
        {
            Mapper = AutoMapper.Mapper.Instance;
        }

        public void Create(ViewType entity)
        {
            RepoType repoEntity = GetRepoEntity(entity);
            OnEntityCreate(repoEntity);
            base.Add(repoEntity);
            Save();
            //to Map ID
            Mapper.Map<RepoType, ViewType>(repoEntity, entity);
        }

        public void Delete(IdType ID)
        {
            RepoType repoEntity = GetByID(ID);
            OnEntityDelete(repoEntity);
            base.Delete(repoEntity);
            Save();
        }

        public void Update(ViewType entity,IdType ID)
        {
            //Get Original Entity First
            RepoType repoEntity = GetByID(ID);
            //Refill it with the Updated Data
            repoEntity = GetRepoEntity(entity, repoEntity);
            Update(repoEntity);
            Save();
        }

        public RepoType GetByID(IdType ID)
        {
            switch (Type.GetTypeCode(ID.GetType()))
            {
                case TypeCode.Int16:
                    return GetByID(Convert.ToInt16(ID));
                case TypeCode.Int32:
                    return GetByID(Convert.ToInt32(ID));
                case TypeCode.Int64:
                    return GetByID(Convert.ToInt64(ID));
            }
            if (ID is Guid)
                return GetByID(Guid.Parse(ID.ToString()));

            throw new Exception("Undefiend Type Yet");
        }

        ViewType IRepositoryBase<ViewType, IdType>.GetByID(IdType ID)
        {
            return GetViewEntity(GetByID(ID));
        }
        #region Events
        public virtual void OnEntityCreate(RepoType entity)
        {

        }

        public virtual void OnEntityDelete(RepoType repoEntity)
        {

        }
        #endregion

        #region Helpers
        protected RepoType GetRepoEntity(ViewType entity)
        {
            return entity.MapTo<RepoType>();
        }
        protected RepoType GetRepoEntity(ViewType entity, RepoType repoType)
        {
            return entity.MapTo<ViewType,RepoType>(repoType);
        }

        protected ViewType GetViewEntity(RepoType entity)
        {
            return entity.MapTo<ViewType>();
        }

        protected ViewType GetViewEntity(RepoType entity, ViewType viewType)
        {
            return entity.MapTo<RepoType, ViewType>(viewType);
        }
        #endregion

    }

    public abstract class EntityRepositoryBase<RepoType, ViewType> :EntityRepositoryBase<RepoType, ViewType, int>
        where RepoType : class
        where ViewType : class
    {

    } 

    public static class Extenstions
    {
        public static Destination MapTo<Destination>(this object obj) {
            if (obj == null)
                return default(Destination);

            return AutoMapper.Mapper.Instance.Map<Destination>(obj);
        }

        public static Destination MapTo<Input,Destination>(this Input obj,Destination destination)
        {
            if (destination.Equals(default(Destination)))
                return obj.MapTo<Destination>();

            return AutoMapper.Mapper.Map<Input, Destination>(obj,destination);
        }
    }
}
