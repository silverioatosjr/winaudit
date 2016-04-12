using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechHelperPOC.Web.Models;
using TechHelperPOC.Web.Repository;

namespace TechHelperPOC.Web.Controllers
{
    public class BaseController : Controller
    {
        protected UnitOfWork unit;

        public BaseController()
            : this(new ApplicationDbContext())
        {
        }

        public BaseController(DbContext context)
        {
            unit = new UnitOfWork(context);
        }
    }
}