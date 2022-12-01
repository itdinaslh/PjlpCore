using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("eventfiles")]
public class EventFile
{
    [Key]
    public Guid EventFileID { get; set; } = Guid.NewGuid();

    public int EventID { get; set; }

    public int PersyaratanID { get; set; }    

    public Guid JabatanID { get; set; }

    public bool IsNew { get; set; }

#nullable disable

    public Event Event { get; set; }

    public Persyaratan Persyaratan { get; set; }

    public Jabatan Jabatan { get; set; }
}
