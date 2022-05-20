using PjlpCore.Entity;

namespace PjlpCore.Models.Wilayah;

public class KecamatanViewModel {
    #nullable disable
    public Kecamatan Kecamatan { get; set; }

    #nullable enable
    public string? NamaKabupaten { get; set; }

    public string? ExistingID { get; set; }

    public bool IsNew { get; set; } = true;
}