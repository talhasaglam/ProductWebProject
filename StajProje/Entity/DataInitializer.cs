using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StajProje.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected  override  void Seed(DataContext context)
        { 

        var kategoriler = new List<Kategori>()
        {
            new Kategori(){ Ad = "Baharatlar"},
            new Kategori(){ Ad = "Pekmezler"},
            new Kategori(){ Ad = "Yumurtalar"}
        };

        foreach (var kategori in kategoriler)
        {
            context.Kategoriler.Add(kategori);
        }

        context.SaveChanges();

        var urunler = new List<Urun>()
        {
            new Urun(){ Ad = "250 gr baharat", Fiyat = 50, Fotograf = "images/product-1.jpg", Stok = 5, Onayli = true, KategoriId = 1},
            new Urun(){ Ad = "500gr pekmez",Fiyat = 60, Fotograf = "images/product-2.jpg", Stok = 5, Onayli = true, KategoriId = 2},
            new Urun(){ Ad = "30 tane yumurta", Fiyat = 70, Fotograf = "images/product-3.jpg", Stok = 5, Onayli = true, KategoriId = 3},
            new Urun(){ Ad = "50 gr ihlamur", Fiyat = 80, Fotograf = "images/product-4.jpg", Stok = 5, Onayli = true, KategoriId = 1}
        };

        foreach (var urun in urunler)
        {
            context.Urunler.Add(urun);

        }

        context.SaveChanges();




            base.Seed(context);
        }
    }
}