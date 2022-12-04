namespace PjlpCore.Models
{
    public class SelectedList
    {
        public int ID { get; set; }
        public string? Text { get; set; }
        public Guid? OtherID { get; set; }

        public bool Selected { get; set; }
    }
}
