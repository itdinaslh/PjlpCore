using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("Provinsi")]
public class Provinsi {
    #nullable disable
    [Key]
    [MaxLength(5, ErrorMessage = "Maksimal 5 Karakter")]
    [Required(ErrorMessage = "Kode Provinsi Wajib Diisi")]
    public string ProvinsiID { get; set; }

    [MaxLength(50)]
    [Required(ErrorMessage = "Nama Provinsi Wajib Diisi")]
    public string NamaProvinsi { get; set; }

    #nullable enable
    [MaxLength(20)]
    public string? HcKey { get; set; }

    [MaxLength(30, ErrorMessage = "Maksimal 30 Karakter")]
    public string? Latitude { get; set; }

    [MaxLength(30, ErrorMessage = "Maksimal 30 Karakter")]
    public string? Longitude { get; set; }

    #nullable disable
    [MaxLength(5)]
    [Required(ErrorMessage = "Kode Negara Wajib Diisi")]
    public string KodeNegara { get; set; } = "360";

    public List<Kabupaten> Kabupatens { get ; set; }

}