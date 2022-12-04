using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PjlpCore.Entity;

[Table("userbidang")]
public class UserBidang
{
    [Key]
    public Guid UserBidangID { get; set; }

    public Guid UserID { get; set; }

    public Guid BidangID { get; set; }

#nullable disable

    public User User { get; set; }

    public Bidang Bidang { get; set; }
}
