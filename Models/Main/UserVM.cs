using PjlpCore.Entity;

namespace PjlpCore.Models;

public class UserVM {
    #nullable disable

    public User User { get; set; }

    public int Batas { get; set; }

    public bool IsNew { get; set; } = true;

    #nullable enable
    public string? Password { get; set; }

    public Guid[]? Bidangs { get; set; }

    #nullable disable
    public List<SelectedList> ListBidang { get; set;}
    
}