using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StajProje.Models
{
    public class Login
    {

        [Required]
        [DisplayName("Eposta")]
        [EmailAddress(ErrorMessage = "Epostayı doğru girmediniz.")]
        public string Email { get; set; }
      
        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}