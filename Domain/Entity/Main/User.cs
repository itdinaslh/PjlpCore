using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("users")]
public class User
{
    [Key]
    public Guid UserID { get; set; } = Guid.Empty;

#nullable disable
    
    [Required(ErrorMessage = "Username wajib diisi!")]
    [MaxLength(100)]
    public string UserName { get; set; }

    [MaxLength(100)]
    [Required(ErrorMessage = "Nama User Wajib Diisi")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Role wajib!")]
    [MaxLength(100)]
    public string RoleName { get; set; }

    [Required(ErrorMessage = "Data Email Wajib Diisi")]
    [MaxLength(255)]
    public string Email { get; set; }

    public bool IsActive { get; set; } = true;

    #nullable enable

    [MaxLength(100)]
    public string? CreatedBy { get; set; }

    [MaxLength(100)]
    public string? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    #nullable disable

    public List<UserBidang> UserBidangs { get; set; }


}
