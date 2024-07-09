using System.ComponentModel.DataAnnotations;

namespace UserManagementMVC.Models;

public class LoginData
{
    //[Required(ErrorMessage ="User name is required")]
    public string? UserName { get; set; }
    //[Required(ErrorMessage = "User name is required")]
    public string? Password { get; set; }
}
