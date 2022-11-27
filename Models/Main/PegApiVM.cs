namespace PjlpCore.Models;

public class PegApiVM {
    public Guid PegawaiID;

    public string? NIK { get; set;}

    public string? NamaPegawai { get; set; }

    public string? NoHP { get; set; }

    public string? Email { get; set; }

    public string? AlamatKTP { get; set; }

    public Guid? BidangID { get; set; }

    public string? NamaBidang { get; set; }

    public string? TglLahir { get; set; }
}