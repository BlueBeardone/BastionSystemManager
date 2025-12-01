namespace Web.ViewModels;
using System.ComponentModel.DataAnnotations;

public class CreateBastionViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}