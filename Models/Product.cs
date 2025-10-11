using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalog.Models;

[Table("Products")]
public class Product : IValidatableObject
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(
        80,
        MinimumLength = 2,
        ErrorMessage = "Name must be between 2 and 80 characters"
    )]
    // [FirstLetterCapitalized]
    public string? Name { get; set; }

    [Required]
    [StringLength(
        300,
        MinimumLength = 10,
        ErrorMessage = "Description must be between 10 and 300 characters"
    )]
    public string? Description { get; set; }

    [Required]
    [StringLength(
        300,
        ErrorMessage = "ImageUrl must have maximum 300 characters"
    )]
    public string? ImageUrl { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 10000, ErrorMessage = "Price must be between {1} and {2}")]
    public decimal Price { get; set; }

    // [Range(
    //     0,
    //     float.MaxValue,
    //     ErrorMessage = "Inventory must be a positive number or zero"
    // )]
    public float Inventory { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }


    // Approach using IValidatableObject.
    // It seems very polluted to create these validations
    // along with the models.
    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext
        )
    {
        if (!string.IsNullOrEmpty(Name))
        {
            var firstLetter = Name[0].ToString();
            if (!string.Equals(
                    firstLetter,
                    firstLetter.ToUpper(),
                    StringComparison.Ordinal
                    )
                )
            {
                yield return new ValidationResult(
                    "The first letter of the name must be capitalized.",
                    [nameof(Name)]
                    );
            }

            if (Inventory < 0)
            {
                yield return new
                    ValidationResult(
                        "Inventory must be a positive number or zero.",
                        [nameof(Inventory)]
                    );
            }
        }
    }
}