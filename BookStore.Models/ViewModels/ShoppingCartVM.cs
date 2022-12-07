using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore.Models.ViewModels
{
    public class ShoppingCartVM
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
    }
}
