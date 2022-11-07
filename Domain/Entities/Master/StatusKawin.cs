using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entities;

[Table("statuskawin")]
public class StatusKawin
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StatusKawinID { get; set; }

#nullable disable

    [Required]
    [MaxLength(30)]
    public string NamaStatus { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}
