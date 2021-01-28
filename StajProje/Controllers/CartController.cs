using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StajProje.Entity;
using StajProje.Models;

namespace StajProje.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Urunler.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
               GetCart().AddProduct(product, 1);
            }

            Session["Count"] = GetCart().CartLines.Count;
            return RedirectToAction("Urunler","Home");

        }

        public JsonResult AddCart(int id)
        {
            var product = db.Urunler.FirstOrDefault(i => i.Id == id);

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }
            Session["Count"] = GetCart().CartLines.Count;
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Urunler.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            // Her gelen kullanıcıya özel oluşturlan session nesneleri
            var cart = (Cart)Session["Cart"];
            

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            //Session["Count"] = cart.CartLines.Count;
            return cart;
        }

        public ActionResult Checkout()
        {
            TempData["Fiyat"] = GetCart().Total().ToString("c");
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();
            

            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır.");
            }

            if (ModelState.IsValid)
            {
                SaveOrder(cart, entity);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(entity);
            }
        }
        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
            var order = new Order();

            order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            order.Username = User.Identity.Name;

            order.AdresBasligi = entity.AdresBasligi;
            order.Adres = entity.Adres;
            order.Sehir = entity.Sehir;
            order.Semt = entity.Semt;
            order.Mahalle = entity.Mahalle;
            order.PostaKodu = entity.PostaKodu;

            order.Orderlines = new List<OrderLine>(); // Boş olamamsı için oluşturmalıyız

            foreach (var pr in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price = pr.Quantity * pr.Product.Fiyat;
                orderline.ProductId = pr.Product.Id;

                order.Orderlines.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }





    }
}