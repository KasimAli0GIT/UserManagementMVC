
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserMgmtDAL.Entities;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    public virtual ICollection<User> Users{ get; set; }
}
