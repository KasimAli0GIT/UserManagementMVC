
using UserMgmtDAL.Entities;

namespace UserMgmtDAL.Repository;
public interface IAuth
{
    User DoLogin(string email, string password);
}
