using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("detailpjlps")]
public class DetailPjlp
{
    [Key]
    public Guid DetailPjlpID { get; set; }

    public Guid PegawaiID { get; set; }

    [MaxLength(5)]
    public string? Tanggungan { get; set; }

    [MaxLength(50)]
    public string? NoBPJSK { get; set; }

    [MaxLength(30)]
    public string? NoSIM { get; set; }

    public DateOnly? MasaBerlakuSIM { get; set; }

    public Guid? JabatanID { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get;}

#nullable disable

    public Pegawai Pegawai { get; set; }

#nullable enable

    public Jabatan? Jabatan { get; set; }

}
