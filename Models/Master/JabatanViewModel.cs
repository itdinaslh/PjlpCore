using PjlpCore.Entity;

namespace PjlpCore.Models.Master;

public class JabatanViewModel {
    #nullable disable
    public Jabatan Jabatan { get; set; }

    #nullable enable
    public string? NamaBidang { get; set; }

    public Guid? ExistingID { get; set; }

    public bool IsNew { get; set; } = true;
}