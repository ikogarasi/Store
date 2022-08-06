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
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(13)]
        public string ISBN { get; set; }
        
        [MaxLength(50)]
        public string Author { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

        [Range(1, 1000)]
        [ValidateNever]
        public double Price50 { get; set; }

        [Range(1, 1000)]
        [ValidateNever]
        public double Price100 { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryModelId { get; set; }
        [ForeignKey("CategoryModelId")]
        [ValidateNever]
        public CategoryModel CategoryModel { get; set; }

        [Required]
        public int CoverTypeModelId { get; set; }
        [ForeignKey("CoverTypeModelId")]
        [ValidateNever]
        public CoverTypeModel CoverTypeModel { get; set; }

    }
}
