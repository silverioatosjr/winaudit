using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHelperPOC.Web.Models;

namespace TechHelperPOC.Web.Repository
{
    public interface ISystemInfoRepository : IBaseRepository<SystemInfo>
    {
        void DeleteByMachineName(string machine);
    }
}
