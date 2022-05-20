using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("agama")]
public class Agama {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AgamaID { get; set; }

    #nullable disable
    [MaxLength(25)]
    [Required(ErrorMessage = "Nama Agama Wajib Diisi")]
    public string NamaAgama { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}