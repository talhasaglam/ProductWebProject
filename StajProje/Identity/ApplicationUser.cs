using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StajProje.Identity
{
    public class ApplicationUser:IdentityUser
    {
       // public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


    }
}