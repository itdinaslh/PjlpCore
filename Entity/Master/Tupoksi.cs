using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("Tupoksi")]
public class Tupoksi {
    [Key]
    public Guid TupoksiID { get; set; } = new Guid();

    #nullable disable
    [MaxLength(100)]
    [Required(ErrorMessage = "Nama Tupoksi Wajib Diisi")]
    public string NamaTupoksi { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    #nullable disable
    public Guid? DivisiID { get; set; }

    public Divisi Divisi { get; set; }
}