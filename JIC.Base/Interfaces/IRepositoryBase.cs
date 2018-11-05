using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IRepositoryBase<Entity,IdType> where Entity : class
    {
        void Create(Entity entity);
        void Update(Entity entity,IdType id);
        void Delete(IdType id);
        Entity GetByID(IdType ID);
    }

    public interface IRepositoryBase<Entity> : IRepositoryBase<Entity,int> where Entity : class
    {

    }
}
