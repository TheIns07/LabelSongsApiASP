using LabelSongsAPI.Models.Relations;

namespace LabelSongsAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SongCategory> SongCategories { get; set; }
    }
}
