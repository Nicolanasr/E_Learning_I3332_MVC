using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_I3332_MVC.Models;
public class Users
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters")]
    [Required]
    public string? FirstName { get; set; }

    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters")]
    [Required]
    public string? LastName { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 3)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    public int RoleId { get; set; }
    public Roles Role { get; set; }

    public int Status { get; set; } = 0; // 0 user registered but still no teacher / student

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}