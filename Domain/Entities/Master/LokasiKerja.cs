using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entities;

[Table("lokasi_kerja")]
public class LokasiKerja
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LokasiKerjaID { get; set; }

#nullable disable

    public string NamaLokasi { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}
