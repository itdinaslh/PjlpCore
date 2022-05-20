using PjlpCore.Entity;

namespace PjlpCore.Models.Wilayah;

public class KelurahanViewModel {
    #nullable disable
    public Kelurahan Kelurahan { get; set; }

    #nullable enable
    public string? NamaKecamatan { get; set; }

    public string? ExistingID { get; set; }

    public bool IsNew { get; set; } = true;
}