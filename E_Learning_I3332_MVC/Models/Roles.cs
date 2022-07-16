
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning_I3332_MVC.Models;
public class Roles
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId { get; set; }

    [Required]
    [RegularExpression("student|teacher|admin", ErrorMessage = "Value should be student|teacher|admin")]
    [StringLength(255)]

    public List<Users>? UsersList { get; set; }

    public string? Name { get; set; }
}