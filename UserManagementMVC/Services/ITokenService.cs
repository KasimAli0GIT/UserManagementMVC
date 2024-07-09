namespace UserManagementMVC.Services;

public interface ITokenService
{
    string GetJwtToken(string email, string role);
}
