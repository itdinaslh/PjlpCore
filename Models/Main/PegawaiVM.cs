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

    public string? Provinsi { get; set; }

    public string? Kabupaten { get; set; }

    public string? Kecamatan { get; set; }

    public string? Kelurahan { get; set; }
}
