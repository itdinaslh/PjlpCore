using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("StatusLamaran")]
public class StatusLamaran
{
#nullable disable
    public int StatusLamaranId { get; set; }

    [MaxLength(50)]
    public string NamaStatus { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<Pelamar> Pelamars { get; set; }
}
