using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("detailasn")]
public class DetailAsn
{
    [Key]
    public Guid DetailAsnID { get; set; }

    public Guid PegawaiID { get; set; }

    [MaxLength(50)]
    public string? NIP { get; set; }

    [MaxLength(50)]
    public string? NRK { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

#nullable disable

    public Pegawai Pegawai { get; set; }
}
