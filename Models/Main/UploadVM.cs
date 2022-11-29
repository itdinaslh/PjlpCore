using System.ComponentModel.DataAnnotations;

namespace PjlpCore.Models;

public class UploadVM
{   

    public string? FileName { get; set; }

    public string? RealName { get; set; }
    
    public string? Extension { get; set; }

    public IFormFile? TheFile { get; set; }

    public int PersyaratanID { get; set; }

    public string? TypeName { get; set; }
}
