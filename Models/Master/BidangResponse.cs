using PjlpCore.Entity;

namespace PjlpCore.Models.Master;

public class BidangResponse {
    public Object Bidang { get; set; }

    public int Total { get; set; }

    public BidangResponse(List<Bidang> bidang, int totalAl) {
        Bidang = bidang.Select(x => new {
            bidangID = x.BidangID,
            namaBidang = x.NamaBidang,
            kepalaBidang = x.KepalaBidang
        });

        Total = totalAl;
    }
}