using Microsoft.EntityFrameworkCore;

namespace UserMgmtDAL.Entities;
public class UserMgmtContext  : DbContext
{
    public UserMgmtContext(DbContextOptions<UserMgmtContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Supervisor" },
            new Role { Id = 3, Name = "User" });

        modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "XAdmin",
            DOB = new DateTime(1998, 2, 3), Email = "xadmin@mail.com", Mobile = "9876787656", RoleId = 1 },
        new User
        {
            Id = 2,
            Name = "USupervisor",
            DOB = new DateTime(1998, 2, 3),
            Email = "usuper@mail.com",
            Mobile = "9876787353",
            RoleId = 2
        },
        new User
        {
            Id = 3,
            Name = "PUser",
            DOB = new DateTime(1998, 2, 3),
            Email = "puser@mail.com",
            Mobile = "6786787656",
            RoleId = 3
        });
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users{ get; set; }
}
