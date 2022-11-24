using PjlpCore.Entity;

namespace PjlpCore.Models.Master;

public class JabatanViewModel {
    #nullable disable
    public Jabatan Jabatan { get; set; } = new Jabatan();

    #nullable enable
    public string? NamaBidang { get; set; }
}