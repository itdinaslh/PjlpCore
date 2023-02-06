using PjlpCore.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("jabatan")]
public class Jabatan {
    [Key]
    public Guid JabatanID { get; set; } = Guid.Empty;

    #nullable disable
    [MaxLength(100)]
    [Required(ErrorMessage = "Nama Jabatan Wajib Diisi")]
    public string NamaJabatan { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    #nullable disable
    public Guid? BidangID { get; set; }

    public Bidang Bidang { get; set; }

    public List<Pelamar> Pelamars { get; set; }

    public List<DetailPjlp> DetailPjlps { get; set; }

    public List<EventFile> EventFiles { get; set; }
    public List<Tupoksi> Tupoksis { get; set; }
}