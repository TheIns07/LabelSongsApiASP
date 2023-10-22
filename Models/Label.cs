namespace LabelSongsAPI.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Composer> Composers { get; set; }

    }
}
