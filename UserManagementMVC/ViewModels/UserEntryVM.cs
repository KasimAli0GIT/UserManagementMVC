using System.ComponentModel.DataAnnotations;

namespace UserManagementMVC.ViewModels;

public class UserEntryVM
{
    public UserEntryVM()
    {
        Roles = new List<Role>
        {
            new Role{ Id=0, Name="Select Role"},
            new Role{ Id=1, Name="Admin"},
            new Role{ Id=2, Name="Supervisor"},
            new Role{ Id=3, Name="User"}
        };
    }

    public int Id { get; set; }
    [Required(ErrorMessage = "Name is a required field")]
    [MinLength(3, ErrorMessage = "Name should be atleast 3 chars")]
    public string Name { get; set; } = "SLK";
    [Required(ErrorMessage ="DOB is required")]
    public DateTime DOB { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^\w+@\w+\.\w{2,3}$",ErrorMessage ="Invalid email format")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Mobile is required")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid mobile format")]
    public string Mobile { get; set; }
  //  public string Gender { get; set; }
    [Required(ErrorMessage = "Role is required")]
    public byte RoleId { get; set; }

    public IEnumerable<Role> Roles { get; set; }


}
