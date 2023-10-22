using LabelSongsAPI.Models.Relations;

namespace LabelSongsAPI.Models
{
    public class Song
    {
        public int ID { get; set;  }
        public string Name { get; set; }
        public DateTime LaunchTime { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<SongComposer> SongComposers { get; set; }
        public ICollection<SongCategory> SongCategories { get; set; }    
    }
}
