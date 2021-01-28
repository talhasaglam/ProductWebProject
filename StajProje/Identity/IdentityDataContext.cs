using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace StajProje.Identity
{
    public class IdentityDataContext :IdentityDbContext<ApplicationUser>
    {

        public IdentityDataContext() : base("dataConnection")
        {
            Database.SetInitializer(new IdentityInitializer());

        }

    }

}