using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("users")]
public class User
{
    [Key]
    public Guid UserID { get; set; } = Guid.Empty;

#nullable disable

    [MaxLength(100)]
    [Required(ErrorMessage = "Nama User Wajib Diisi")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Role wajib!")]
    public int RoleID { get; set; }

    public bool IsActive { get; set; } = true;

    #nullable enable

    [MaxLength(100)]
    public string? CreatedBy { get; set; }

    [MaxLength(100)]
    public string? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    #nullable disable

    public ICollection<Bidang> Bidangs { get; set; }
}
