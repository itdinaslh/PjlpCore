using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("bidang")]
public class Bidang {
    [Key]
    public Guid BidangID { get; set; } = new Guid();

    #nullable disable
    [Required(ErrorMessage = "Nama Bidang Wajib Diisi")]
    [MaxLength(75)]
    public string NamaBidang { get; set; }    

    #nullable enable
    [MaxLength(75)]
    public string? KepalaBidang { get; set; }

    public bool? IsVisible { get; set; } = true;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<Divisi>? Divisis { get; set; }
    
    public List<Jabatan>? Jabatans { get; set; }

#nullable disable

    public List<Pelamar> Pelamars { get; set; }

    public List<Pegawai> Pegawais { get; set; }

    public List<UserBidang> UserBidangs { get; set; }
    
}