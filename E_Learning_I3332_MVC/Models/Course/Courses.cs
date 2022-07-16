using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_I3332_MVC.Models;
public class Courses
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 1)]
    public string? CourseName { get; set; }

    [Required]
    [Range(1, 14)]
    public int? Credits { get; set; }

    [Required]
    public Specializations? Specialization { get; set; }
    public int? SpecializationId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

}