using PjlpCore.Entity;

namespace PjlpCore.Models;

public class DashboardVM {

    public int CountBidang;    

    public int CountPelamar;

    public int CountBaru;

    public int CountLama;

    #nullable disable
    public List<PelamarDashVM> PelamarLama { get; set; }

    public List<PelamarDashVM> PelamarBaru { get; set; }
}