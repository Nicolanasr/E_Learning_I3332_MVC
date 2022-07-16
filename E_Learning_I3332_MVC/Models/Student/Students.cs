
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_I3332_MVC.Models;
public class Students
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentId { get; set; }

    [Required]
    public Users? User { get; set; }
    public int? UserId { get; set; }

    public int PhoneNumer { get; set; }

    [Required]
    public Specializations? Specialization { get; set; }
    public int? SpecializationId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime JoinedDate { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}