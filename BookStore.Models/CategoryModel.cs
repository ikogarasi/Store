using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Display order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100 only!")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
