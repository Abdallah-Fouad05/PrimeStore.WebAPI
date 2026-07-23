using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeStore.data.Entities;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    [StringLength(500)]
    public required string CategoryName { get; set; }

    public string? ImageUrl { get; set; }

    public int? ParentCategoryID { get; set; }

    public Category? ParentCategory { get; set; }

    public ICollection<Category>? ChildCategories { get; set; } = new List<Category>();

    public ICollection<Product>? Products { get; set; } = new List<Product>();

}