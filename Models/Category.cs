using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalog.Models;

[Table("Categories")]
public class Category
{
    public Category()
    {
        Products = [];
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must have between 2 and 80 characters")]
    public string? Name { get; set; }

    [Required]
    [StringLength(300, ErrorMessage = "ImageUrl must have maximum 300 characters")]
    public string? ImageUrl { get; set; }

    public ICollection<Product>? Products { get; set; }
}