using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Metadata;
using UserMgmtDAL.Entities;

namespace UserMgmtDAL.Repository;
public class UserRepository : IRepository<User>
{
    private readonly UserMgmtContext context;
    public UserRepository(UserMgmtContext context)
    {
        this.context = context;
    }

    public int Add(User item)
    {
        context.Users.Add(item);
        int noofrowaff = context.SaveChanges();
        return item.Id;
    }

    public bool Delete(int id)
    {
        var user = GetById(id);
        context.Users.Remove(user);
        int noofrowaff = context.SaveChanges();
        if (noofrowaff > 0)
            return true;
        else
            return false;
    }

    public IEnumerable<User> GetAll()
    {
        IEnumerable<User> users = from User item in context.Users.Include("Role")
                                  orderby item.Name ascending
                                  select item;
        //users.Skip(20).Take(10)
        return users;
    }

    public User GetById(int id)
    {
        IEnumerable<User> users = context.Users.Include("Role")
                                    .Where(u => u.Id == id);

        return users.SingleOrDefault()!;
    }

    public IEnumerable<User> GetByName(string name)
    {
        //IEnumerable<User> users = from User item in context.Users.Include("Role")
        //                          where item.Name.ToLower().StartsWith(name.ToLower())
        //                          select item;
        IEnumerable<User> users = context.Users.Include("Role")
                                    .Where(u => u.Name.ToLower().StartsWith(name.ToLower()));
                                   
        return users;
    }

    public bool Update(User item)
    {
        var user = GetById(item.Id);

        user.Mobile = item.Mobile;
        int noofrowaff = context.SaveChanges();
        if (noofrowaff > 0)
            return true;
        else
            return false;
    }
}
