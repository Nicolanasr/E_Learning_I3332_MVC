using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_I3332_MVC.Models;
public class Specializations
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SpecializationId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 1)]
    public string? SpecializationName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}