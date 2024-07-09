
using Microsoft.EntityFrameworkCore;
using UserMgmtDAL.Entities;

namespace UserMgmtDAL.Repository;
public class AuthRepository : IAuth
{
    private readonly UserMgmtContext mgmtContext;

    public AuthRepository(UserMgmtContext mgmtContext)
    {
        this.mgmtContext = mgmtContext;
    }
    public User DoLogin(string email, string password)
    {
        var user = mgmtContext.Users.Include("Role").
                SingleOrDefault(u => u.Email.ToLower() == email.ToLower());//passwrd has to be compared

        return user!;
    }
}
