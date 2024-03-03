using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]  // For the Razor Page
        public string? Name { get; set; }

        [Required]
        [DisplayName("Display Order")]  //labels on form
        public int DisplayOrder { get; set; }
    }
}