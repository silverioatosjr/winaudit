using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechHelperPOC.Web.Models;

namespace TechHelperPOC.Web.Repository
{
    public interface IBaseRepository<T> where T : IEntity
    {
        List<T> Get();
        T Get(int id);
        T Get(string id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(string id);
        void Delete(T entity);
    }
}