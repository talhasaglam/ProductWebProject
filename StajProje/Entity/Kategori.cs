using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProje.Entity
{
    public class Kategori
    {

        public int Id { get; set; }
        [DisplayName("Kategori Adı")] //Ad yerine burda yazdığımız kategori adını çekiyor viewde.
        [StringLength(maximumLength:20,ErrorMessage = "en fazla 20 karakter gir")] // Data annatation ARAŞTIR
        public string Ad { get; set; }

        public List<Urun> Urunler { get; set; }

    }
}