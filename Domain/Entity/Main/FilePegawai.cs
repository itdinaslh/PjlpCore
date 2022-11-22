using System.ComponentModel.DataAnnotations;

namespace PjlpCore.Domain.Entity.Main
{
    public class FilePegawai
    {
        [Key]
        public Guid FilePegawaiID { get; set; } = Guid.Empty;

#nullable disable

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [DataType(DataType.Text)]
        public string RealName { get; set; }

        [DataType(DataType.Text)]
        public string FilePath { get; set; }

        [MaxLength(10)]
        public string FileExtension { get; set; }
    }
}
