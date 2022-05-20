using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("kabupaten")]
public class Kabupaten {
    #nullable disable
    [Key]
    [Required(ErrorMessage = "Kode Kota/Kabupaten Wajib Diisi")]
    [MaxLength(10, ErrorMessage = "Maksimal 10 Karakter")]
    public string KabupatenID { get; set; }

    [MaxLength(150)]
    [Required(ErrorMessage = "Nama Kota/Kabupaten Wajib Diisi")]
    public string NamaKabupaten { get; set; }

    public bool IsKota { get; set; } = false;

    #nullable enable
    [MaxLength(30, ErrorMessage = "Maksimal 30 Karakter")]
    public string? Latitude { get; set; }

    [MaxLength(30, ErrorMessage = "Maksimal 30 Karakter")]
    public string? Longitude { get; set; }

    #nullable disable
    [MaxLength(5)]
    public string ProvinsiID { get; set; }

    public Provinsi Provinsi { get; set; }

    public List<Kecamatan> Kecamatans { get; set; }
}