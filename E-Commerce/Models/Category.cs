using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    [DisplayName("Category Name")]
    public string Name { get; set; }
    [DisplayName("Display Order")]
    public int DisplayOrder {  get; set; }
}
