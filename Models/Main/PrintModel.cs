using PjlpCore.Entity;

namespace PjlpCore.Models;

public class PrintModel
{
    #nullable disable
    public Pelamar Pelamar { get; set; }

    public string ImageProfile { get; set; } = "/img/user.png";

    public string StatusBerkas { get; set; } = "Belum Lengkap";
}
