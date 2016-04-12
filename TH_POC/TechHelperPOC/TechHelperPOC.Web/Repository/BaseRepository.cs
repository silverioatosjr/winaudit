using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TechHelperPOC.Web.Models;

namespace TechHelperPOC.Web.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T: IEntity
    {
        protected DbSet<T> dbSet;
        private DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public List<T> Get()
        {
            return dbSet.ToList();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public T Get(string id)
        {
            return dbSet.Find(id);
        }

       public void Create(T entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public void Update(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            var entity = dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(string id)
        {
            var entity = dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            try
            {
                dbSet.Remove(entity);
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }
    }
}