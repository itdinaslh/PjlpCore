using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("pelamar")]
public class Pelamar
{
    [Key]
    public Guid PelamarId { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

#nullable disable

    [MaxLength(255)]
    [Required]
    public string UserEmail { get; set; }

    public int EventId { get; set; }

    [MaxLength(16, ErrorMessage = "No KTP maksimal 16 karakter")]
    [MinLength(16, ErrorMessage = "No KTP minimal 16 karakter")]
    [Required(ErrorMessage = "Data Wajib Diisi")]
    public string NoKTP { get; set; }

    [MaxLength(150)]
    [Required(ErrorMessage = "Nama Wajib Diisi")]
    public string Nama { get; set; }

    [Required(ErrorMessage = "Agama wajib diisi")]
    public int AgamaId { get; set; }

#nullable enable

    [Required(ErrorMessage = "Tanggal lahir wajib diisi")]
    public DateOnly? TglLahir { get; set; }

#nullable disable

    [MaxLength(255)]
    [Required(ErrorMessage = "Tempat lahir wajib diisi")]
    public string TempatLahir { get; set; }

    // 1 untuk laki-laki dan 0 untuk perempuan, jenis kelamin diciptain cuma 2 jd gak ada kelamin selain 2 itu
    [Required(ErrorMessage = "Jenis kelamin wajib dipilih")]
    public bool Kelamin { get; set; }

    [Required(ErrorMessage = "Alamat wajib diisi")]
    public string Alamat { get; set; }

#nullable enable

    [MaxLength(5)]
    public string? RT { get; set; }

    [MaxLength(5)]
    public string? RW { get; set; }

#nullable disable

    [MaxLength(15)]
    [Required(ErrorMessage = "Kelurahan wajib dipilih")]
    public string KelurahanId { get; set; }

#nullable enable

    [MaxLength(10)]
    public string? KodePos { get; set; }

    [MaxLength(15)]
    public string? DomKelurahanId { get; set; }

    public string? DomAlamat { get; set; }

    [MaxLength(5)]
    public string? DomRT { get; set; }

    [MaxLength(5)]
    public string? DomRW { get; set; }

    [MaxLength(10)]
    public string? DomKodePos { get; set; }

#nullable disable

    public Guid JabatanId { get; set; }

    public int PendidikanId { get; set; }

#nullable enable

    [MaxLength(150)]
    public string? NamaSekolah { get; set; }

    [MaxLength(150)]
    public string? JurusanSekolah { get; set; }

    [MaxLength(30)]
    public string? NoSIM { get; set; }

    public DateOnly? TglAkhirSIM { get; set; }

    [MaxLength(20)]
    public string? NoKK { get; set; }

    [MaxLength(4)]
    public string? GolonganDarah { get; set; }

    [MaxLength(30)]
    public string? NoRekening { get; set; }

    [MaxLength(100)]
    public string? CabangRekening { get; set; }

#nullable disable

    public Guid BidangId { get; set; }

#nullable enable

    [MaxLength(20)]
    public string? Telp { get; set; }

    [MaxLength(2)]
    public string? Tanggungan { get; set; }

#nullable disable

    [MaxLength(30)]
    public string NoNPWP { get; set; }

#nullable enable

    [MaxLength(45)]
    public string? NoBPJS { get; set; }

    [MaxLength(45)]
    public string? NoBPJSK { get; set; }

    [MaxLength(45)]
    public string? StatusBPJS { get; set; }

#nullable disable

    public bool IsNew { get; set; } = false;

    public int StatusLamaranId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public Event Event { get; set; }

    public Agama Agama { get; set; }

    public Kelurahan Kelurahan { get; set; }

    public Jabatan Jabatan { get; set; }

    public Pendidikan Pendidikan { get; set; }

    public Bidang Bidang { get; set; }

    public StatusLamaran StatusLamaran { get; set; }

    public Kelurahan KelurahanDom { get; set; }
    
}
