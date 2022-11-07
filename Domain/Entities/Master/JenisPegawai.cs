using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entities;

[Table("jenis_pegawai")]
public class JenisPegawai
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int JenisPegawaiID { get; set; }

#nullable disable

    [MaxLength(30)]
    public string NamaJenis { get; set; }

}
