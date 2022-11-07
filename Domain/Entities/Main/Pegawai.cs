using PjlpCore.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entities;

[Table("pegawai")]
public class Pegawai
{
    [Key]
    public Guid PegawaiID { get; set; } = Guid.Empty;

    // Reference To NIP or id of PJLP
    [MaxLength(50)]
    public string? UniqueID { get; set; }

    public string? Email { get; set; }

#nullable disable
    [MaxLength(16)]
    [Required(ErrorMessage = "No KTP Wajib Diisi!")]
    public string NoKTP { get; set; }

    public int JenisPegawaiID { get; set; }

    public Guid BidangID { get; set; }

#nullable enable
    [MaxLength(16)]
    public string? NoKK { get; set; }

    [MaxLength(25)]
    public string? NPWP { get; set; }

    [MaxLength(15)]
    public string? NoHP { get; set; }

    [MaxLength(30)]
    public string? TempatLahir { get; set; }

    public DateOnly? TglLahir { get; set;  }

    // false 0 if woman or girl and true 1 if man
    public bool Kelamin { get; set; }

    [Required(ErrorMessage = "Agama Wajib Diisi!")]
    public int AgamaID { get; set; }

    public int? PendidikanID { get; set; }

    public int? StatusKawinID { get; set; }

    // true if K2 false if non k2
    public bool? IsK2 { get; set; }

    public bool? IsKtpDKI { get; set; }

    [MaxLength(30)]
    public string? NoRekeningDKI { get; set; }

    [MaxLength(100)]
    public string? CabangBankDKI { get; set; }

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

    public Guid? JabatanID { get; set; }

#nullable disable

    public Agama Agama { get; set; }

    public JenisPegawai JenisPegawai { get; set; }

#nullable enable

    public Pendidikan? Pendidikan { get; set; }

    public Jabatan? Jabatan { get; set; }

    public StatusKawin? StatusKawin { get; set; }

    public Kelurahan? Kelurahan { get; set; }

    public Kelurahan? KelurahanDom { get; set; }
}
