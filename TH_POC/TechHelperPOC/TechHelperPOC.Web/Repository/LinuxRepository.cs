using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TechHelperPOC.Web.Models;

namespace TechHelperPOC.Web.Repository
{
    public class LinuxRepository : BaseRepository<LinuxInfo>, ILinuxRepository
    {
        public LinuxRepository(DbContext context)
            : base(context)  
        {
        }

        public void DeleteByMachineName(string machine)
        {
            var infos = Get().Where(e => e.MachineName == machine);

            foreach (var info in infos)
            {
                Delete(info);
            }
        }
    }
}