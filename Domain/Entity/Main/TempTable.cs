using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("temptable")]
public class TempTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TempID { get; set; }

    public string? NIK { get; set; }

    public Guid? Kode { get; set; }
}
