using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserMgmtDAL.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column(TypeName = "nvarchar")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName ="date")]
    public DateTime DOB { get; set; }


    
    //[Column(TypeName = "date")]
    //public DateTime? DOJ { get; set; }

    [Required]
    [StringLength(50)]
    [Column(TypeName = "nvarchar")]
    public string Email { get; set; }
    [Required]
    [StringLength(10)]
    [Column(TypeName = "nchar")]
    public string Mobile { get; set; }

    [ForeignKey("Role")]
    public byte RoleId { get; set; }

    public virtual Role Role { get; set; }
}