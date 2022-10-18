using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("statuslamaran")]
public class Status
{
#nullable disable
    public int StatusId { get; set; }

    [MaxLength(50)]
    public string NamaStatus { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<Pelamar> Pelamars { get; set; }
}
