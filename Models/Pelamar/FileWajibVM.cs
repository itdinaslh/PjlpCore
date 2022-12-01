using PjlpCore.Entity;

namespace PjlpCore.Models
{
    public class FileWajibVM
    {
        public List<SelectedList>? ListSyarat { get; set; }

        public Guid? BidangID { get; set; }

        public Guid JabatanID { get; set; }

        public bool IsNew { get; set; }

        public int[]? Files { get; set; }
    }
}
