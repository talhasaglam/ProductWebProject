using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StajProje.Entity
{
    public class Urun
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public double Fiyat { get; set; }
        public string Fotograf { get; set; }
        public int Stok { get; set; }
        public bool Onayli { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
    }
}