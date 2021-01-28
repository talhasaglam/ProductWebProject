using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProje.Entity
{
    public enum EnumOrderState
    {   [Display(Name = "Onay bekleniyor.")]
        Waiting,
        [Display(Name = "Tamamlandı.")]
        Completed

        //Kargo Onayı beklenyior vs vs gibi durumlar gösterebiliriz :)

    }
}