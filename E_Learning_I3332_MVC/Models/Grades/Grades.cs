using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_I3332_MVC.Models;
public class Grades
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GradeId { get; set; }

    [Required]
    public int Grade { get; set; } = 0;

    [Required]
    public int GradeTotal { get; set; } = 100;

    [Required]
    public int GradePercentage { get; set; } = 100;

    [Required]
    [StringLength(255, MinimumLength = 1)]
    public string? Type { get; set; }

    [Required]
    public int StudentCourseId { get; set; }
    public StudentCourses StudentCourse { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

}