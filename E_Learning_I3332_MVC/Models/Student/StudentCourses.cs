using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_I3332_MVC.Models;
public class StudentCourses
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public Students? Student { get; set; }
    public int? StudentId { get; set; }

    [Required]
    public Courses? Course { get; set; }
    public int? CourseId { get; set; }

    [Required]
    [Range(1900, 2100)]
    public int Year { get; set; }

    [Required]
    [RegularExpression("winter|fall|summer", ErrorMessage = "Value should be winter|fall|summer")]
    public string? Semester { get; set; }

    [DataType(DataType.Date)]
    [Required]
    public DateTime EntolementDate { get; set; } = DateTime.Now;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

}