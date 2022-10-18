using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Domain.Entities;

[Table("status_pegawai")]
public class StatusPegawai
{
    public int StatusPegawaiId { get; set; }

#nullable disable
    [Required(ErrorMessage = "Nama Status Wajib Diisi")]
    [MaxLength(30)]
    public string NamaStatus { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}
