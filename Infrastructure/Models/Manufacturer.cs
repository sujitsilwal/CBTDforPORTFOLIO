using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Infrastructure.Models;

public class Manufacturer
{
    [Key]
    public int Id { get; set; }

    [Required]  // For the Razor Page
    public string? Name { get; set; }
}