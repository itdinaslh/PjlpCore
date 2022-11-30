using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("filepegawais")]
public class FilePegawai
{
    [Key]
    public Guid FilePegawaiID { get; set; } = Guid.Empty;

#nullable disable

    public Guid PegawaiID { get; set; }

    public int PersyaratanID { get; set; }

    [Required]
    [MaxLength(255)]
    public string FileName { get; set; }

    [DataType(DataType.Text)]
    public string RealName { get; set; }

    [DataType(DataType.Text)]
    public string FilePath { get; set; }

    [DataType(DataType.Text)]
    public string RealPath { get; set; }

    [MaxLength(10)]
    public string FileExtension { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public Pegawai Pegawai { get; set; }

    public Persyaratan Persyaratan { get; set; }
}
