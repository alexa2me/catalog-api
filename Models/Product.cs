using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalog.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must have maximum 80 characters")]
    public string? Name { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 10, ErrorMessage = "Description must have maximum 300 characters")]
    public string? Description { get; set; }

    [Required]
    [StringLength(300, ErrorMessage = "ImageUrl must have maximum 300 characters")]
    public string? ImageUrl { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 10000, ErrorMessage = "Price must be between {1} and {2}")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Inventory must be a positive number or zero")]
    public int Inventory { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }
}