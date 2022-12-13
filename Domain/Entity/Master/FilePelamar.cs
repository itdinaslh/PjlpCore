using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity
{
    [Table("filepelamar")]
    public class FilePelamar
    {
        [Key]
        public Guid FilePelamarID { get; set; } = Guid.Empty;

        public Guid PelamarId { get; set; }

#nullable disable

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

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Pelamar Pelamar { get; set; }

        public Persyaratan Persyaratan { get; set; }
    }
}
