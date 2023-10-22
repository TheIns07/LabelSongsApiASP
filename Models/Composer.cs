using LabelSongsAPI.Models.Relations;

namespace LabelSongsAPI.Models
{
    public class Composer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Label Label { get; set; }
        public ICollection<SongComposer> SongComposers { get; set; }   

    }
}
