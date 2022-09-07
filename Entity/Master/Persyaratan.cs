using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("Persyaratan")]
public class Persyaratan {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PersyaratanID { get; set; }

    #nullable disable
    [MaxLength(25)]
    [Required(ErrorMessage = "Nama Persyaratan Wajib Diisi")]
    public string NamaPersyaratan { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}