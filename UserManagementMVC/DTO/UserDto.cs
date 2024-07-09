namespace UserManagementMVC.DTO;

public class UserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime DOB { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public byte RoleId { get; set; }

    public string? RoleName { get; set; }
}
