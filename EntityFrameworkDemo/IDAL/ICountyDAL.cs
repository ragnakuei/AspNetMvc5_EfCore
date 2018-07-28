using System;
using System.Collections.Generic;
using EntityFrameworkDemo.Models.EntityModel;

namespace EntityFrameworkDemo.IDAL
{
    public interface ICountyDAL
    {
        IEnumerable<County> Get();
        County              Get(Guid      id);
        bool                Add(County    entity);
        bool                Update(County result);
        bool                Delete(Guid   id);
    }
}