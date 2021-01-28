using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using StajProje.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StajProje.Identity
{
    public class IdentityInitializer: CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            //Rolleri

            if (!context.Roles.Any(i=>i.Name=="admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "admin", Description = "admin rolü" };
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() {Name = "user", Description = "kullanııcı rolü"};
                manager.Create(role);
            }

            if (!context.Users.Any(i => i.Name == "talhasaglam"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser(){ Name = "Talha",Surname="Saglam",Email = "saglam_talha@hotmail.com", PhoneNumber = "05222222222"};
                manager.Create(user, "1234567");
            //    manager.AddToRole(user.Id, "admin");
                //manager.AddToRole(user.Id, "user");
            }

            if (!context.Users.Any(i => i.Name == "mustafatas"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "mustafa", Surname = "tas", Email = "mustafatas@hotmail.com", PhoneNumber = "05222222222"};
                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "user");
            }




            base.Seed(context);
        }
    }
}