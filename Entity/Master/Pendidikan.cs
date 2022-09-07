using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("Pendidikan")]
public class Pendidikan {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PendidikanID { get; set; }

    #nullable disable
    [MaxLength(25)]
    [Required(ErrorMessage = "Nama Pendidikan Wajib Diisi")]
    public string NamaPendidikan { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<Pelamar> Pelamars { get; set; }
}