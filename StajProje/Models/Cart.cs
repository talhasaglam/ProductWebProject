using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using StajProje.Entity;

namespace StajProje.Models
{
    public class Cart
    {

         private List<CartLine> _cardLines = new List<CartLine>();

         //Tabi public olayı daha kurallara baklıarak denetlenmeli.
        public List<CartLine> CartLines
        {
            get { return _cardLines; }
        }

        public void AddProduct(Urun product, int quantity)
        {
            var line = _cardLines.FirstOrDefault(i => i.Product.Id == product.Id);
            if (line == null)
            {
                _cardLines.Add(new CartLine() {Product = product, Quantity = quantity});
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void DeleteProduct(Urun product)
        {
            _cardLines.RemoveAll(i => i.Product.Id == product.Id);
        }

        public double Total()
        {
            return _cardLines.Sum(i => i.Product.Fiyat * i.Quantity);
        }

        public void Clear()
        {
            _cardLines.Clear();
        }

    }

    public class CartLine
    {
        public Urun Product { get; set; }
        public int Quantity { get; set; }
    }
}