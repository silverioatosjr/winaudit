using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TechHelperPOC.Web.Models;

namespace TechHelperPOC.Web.Repository
{
    public class UnitOfWork : IDisposable
    {
        private DbContext context;

        public UnitOfWork()
            : this(new ApplicationDbContext())
        {
        }

        public UnitOfWork(DbContext context)
        {
            this.context = context;
            SystemInfoes = new SystemInfoRepository(context);
        }

        public SystemInfoRepository SystemInfoes { get; set; }

        public void Save()
        {
            this.context.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                GC.SuppressFinalize(this);
            }
            disposed = true;
        }
    }
}