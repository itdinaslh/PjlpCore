using PjlpCore.Entity;

namespace PjlpCore.Models.Wilayah;

public class KabupatenViewModel {
    #nullable disable
    public Kabupaten Kabupaten { get; set; }

    #nullable enable
    public string? NamaProvinsi { get; set; }

    public string? ExistingID { get; set; }

    public bool IsNew { get; set; } = true;
}