using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("pegawai")]
public class Pegawai
{
    [Key]
    public Guid PegawaiID { get; set; } = Guid.Empty;    

    public string? Email { get; set; }

#nullable disable
    [MaxLength(16)]
    [Required(ErrorMessage = "No KTP Wajib Diisi!")]
    public string NIK { get; set; }

    [MaxLength(255)]
    [Required(ErrorMessage = "Nama Pegawai Wajib Diisi")]
    public string NamaPegawai { get; set; }

    public int JenisPegawaiID { get; set; }

    public Guid BidangID { get; set; }

#nullable enable
    [MaxLength(20)]
    public string? NoKK { get; set; }

    [MaxLength(25)]
    public string? NPWP { get; set; }

    [MaxLength(50)]
    public string? NoHP { get; set; }

    [MaxLength(30)]
    public string? TempatLahir { get; set; }

    public DateOnly? TglLahir { get; set;  }

    // false 0 if woman or girl and true 1 if man
    public bool? Kelamin { get; set; }
    
    public int? AgamaID { get; set; }

    public int? PendidikanID { get; set; }

    [MaxLength(100)]
    public string? JurusanPendidikan { get; set; }

    [MaxLength(150)]
    public string? NamaSekolah { get; set; }

    public int? StatusKawinID { get; set; }

    [MaxLength(30)]
    public string? NoRekening { get; set; }

    [MaxLength(100)]
    public string? CabangBank { get; set; }

    // Handle Alamat KTP

    [DataType(DataType.Text)]
    public string? AlamatKTP { get; set; }

    [MaxLength(15)]
    public string? KelurahanID { get; set; }

    [MaxLength(3)]
    public string? RtKTP { get; set; }

    [MaxLength(3)]
    public string? RwKTP { get; set; }

    // Handle Alamat Domisili
    [DataType(DataType.Text)]
    public string? AlamatDom { get; set; }

    [MaxLength(15)]
    public string? KelurahanDomID { get; set; }

    [MaxLength(3)]
    public string? RtDom { get; set; }

    [MaxLength(3)]
    public string? RwDom { get; set; }

#nullable disable

    public Agama Agama { get; set; }

    public JenisPegawai JenisPegawai { get; set; }

    public Bidang Bidang { get; set; }

#nullable enable

    public Pendidikan? Pendidikan { get; set; }

    public StatusKawin? StatusKawin { get; set; }

    public Kelurahan? Kelurahan { get; set; }

    public Kelurahan? KelurahanDom { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}
