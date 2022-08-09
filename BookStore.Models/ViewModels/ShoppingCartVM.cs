using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public ProductModel Product { get; set; }
        [Range(1, 1000)]
        public int Quantity { get; set; }
    }
}
