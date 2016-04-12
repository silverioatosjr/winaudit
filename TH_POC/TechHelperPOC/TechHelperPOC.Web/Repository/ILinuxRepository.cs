using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechHelperPOC.Web.Models;

namespace TechHelperPOC.Web.Repository
{
    public interface ILinuxRepository : IBaseRepository<LinuxInfo>
    {
        void DeleteByMachineName(string machine);
    }
}