using PjlpCore.Entity;

namespace PjlpCore.Models.Master;

public class TupoksiViewModel {
    #nullable disable
    public Tupoksi Tupoksi { get; set; }

    #nullable enable
    public string? NamaJabatan { get; set; }

    public Guid? ExistingID { get; set; }

    public bool IsNew { get; set; } = true;
}