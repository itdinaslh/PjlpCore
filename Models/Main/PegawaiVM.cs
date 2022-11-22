using PjlpCore.Entity;

namespace PjlpCore.Models;

public class PegawaiVM
{
#nullable disable

    public Pegawai Pegawai { get; set; }

#nullable enable
    public string? NamaAgama { get; set; }    

    public string? NamaBidang { get; set; }

    public string? NamaPendidikan { get; set; }

    public string? TanggalLahir { get; set; }

    public string? ProvID { get; set; }

    public string? Provinsi { get; set; }

    public string? KabID { get; set; }

    public string? Kabupaten { get; set; }

    public string? KecID { get; set; }

    public string? Kecamatan { get; set; }

    public string? Kelurahan { get; set; }

    public string? ProvDomID { get; set; }

    public string? ProvinsiDom { get; set; }

    public string? KabDomID { get; set; }

    public string? KabupatenDom { get; set; }

    public string? KecDomID { get; set; }

    public string? KecamatanDom { get; set; }

    public string? KelurahanDom { get; set; }
    
    public List<FilePegawai>? Files { get; set; }

#nullable disable
    public bool IsSame { get; set; }

    public UploadVM Upload { get; set; } = new UploadVM();

}
