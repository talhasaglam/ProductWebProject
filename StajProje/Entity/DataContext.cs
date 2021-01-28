using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StajProje.Entity
{
    public class DataContext: DbContext
    {
        public DataContext(): base("dataConnection")
        {
            Database.SetInitializer(new DataInitializer());

        }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

    }
}