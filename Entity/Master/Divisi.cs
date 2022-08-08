using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("divisi")]
public class Divisi {
    [Key]
    public Guid DivisiID { get; set; } = Guid.Empty;

    #nullable disable
    [MaxLength(100)]
    [Required(ErrorMessage = "Nama Divisi Wajib Diisi")]
    public string NamaDivisi { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    #nullable disable    
    public Guid? BidangID { get; set; }

    public Bidang Bidang { get; set; }
}