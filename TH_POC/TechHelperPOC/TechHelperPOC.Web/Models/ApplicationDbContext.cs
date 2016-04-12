using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TechHelperPOC.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("TechHelperConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<SystemInfo> SystemInfoes { get; set; }
        //public virtual DbSet<SelectedComputer> SelectedComputers { get; set; }
        public virtual DbSet<LinuxInfo> LinuxInfoes { get; set; }
        public virtual DbSet<Auditor> Auditors { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}