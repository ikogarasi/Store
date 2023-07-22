using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        [ValidateNever]
        public ProductModel Product { get; set; }
        
        [Range(1, 1000)]
        public int Quantity { get; set; }

        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        [ValidateNever]
        public UserDataModel User { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
