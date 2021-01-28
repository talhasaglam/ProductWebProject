using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StajProje.Entity;
using StajProje.Identity;
using StajProje.Models;

namespace StajProje.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private DataContext db = new DataContext(); // Küçük projelerde kullanılan çözümler büyül projeler farklı
        // Ripozötör pattern daha kolay ve sağlıklı
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            _userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            _roleManager = new RoleManager<ApplicationRole>(roleStore);
        }


        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders = db.Orders
                .Where(i => i.Username == username)
                .Select(i => new UserOrderModel()
                {
                    Id = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Total = i.Total
                }).OrderByDescending(i => i.OrderDate).ToList();

            //Aslında burda istediğimiz kadarınızı seçiyoruz.

            return View(orders);
        }


        public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.Id,
                    OrderNumber = i.OrderNumber,
                    Total = i.Total,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    AdresBasligi = i.AdresBasligi,
                    Adres = i.Adres,
                    Sehir = i.Sehir,
                    Semt = i.Semt,
                    Mahalle = i.Mahalle,
                    PostaKodu = i.PostaKodu,
                    Orderlines = i.Orderlines.Select(a => new OrderLineModel()
                    {
                        ProductId = a.ProductId,
                        ProductName = a.Product.Ad.Length > 50 ? a.Product.Ad.Substring(0, 47) + "..." : a.Product.Ad,
                        Image = a.Product.Fotograf,
                        Quantity = a.Quantity,
                        Price = a.Price
                    }).ToList()
                }).FirstOrDefault();

            return View(entity);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {

            if(ModelState.IsValid)
            { 
            var user = new ApplicationUser();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.Phone;
            var result = _userManager.Create(user, model.Password);

            if (result.Succeeded)
            {
                //Kullanıcı oluştu kullanıcıyı bir role ata
                if (_roleManager.RoleExists("user"))
                {
                    _userManager.AddToRole(user.Id, "user");
                }

                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("RegisterUserError","Kullanıcı Oluşturma Hatası");
                return RedirectToAction("Index", "Kategori");
            }

            }
            return View(model);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {

            if (ModelState.IsValid)
            {
                var user = _userManager.Find(model.Email, model.Password);

                if (user != null)
                {
                    // Var olan kullanıcıyı dahil et sisiteme, APPLİCATİON COOKİE oluşturup sisteme bırak.
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = _userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe; //Cookileri tutcakmı kullanıcı bakcak ona

                    authManager.SignIn(authProperties, identityclaims);
                    Session["Ad"] = user.Name;
                    Session["Soyad"] = user.Surname; // Bunları kullanmayabiliriz bakacağız.

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("LoginUserError", "böyle kullanıcı yok");
                    return RedirectToAction("Index", "Kategori");
                }

            }

 
                return View(model);
            
        }

        public ActionResult Logout()
        {

            var authManager = HttpContext.GetOwinContext().Authentication;// Cookie
            authManager.SignOut();

            return RedirectToAction("Index","Home");
        }

    }
}