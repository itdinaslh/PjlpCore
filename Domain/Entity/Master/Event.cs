using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("events")]
public class Event
{
#nullable disable
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EventId { get; set; }

    [MaxLength(150)]
    [Required]
    public string EventName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<Pelamar> Pelamars { get; set; }

    public List<EventFile> EventFiles { get; set; }
}
