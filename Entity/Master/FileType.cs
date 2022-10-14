using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("filetypes")]
public class FileType
{
#nullable disable

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FileTypeId { get; set; }

    [Required(ErrorMessage = "Jenis File Wajib Diisi")]
    public string TypeName { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}
