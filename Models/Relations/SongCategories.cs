namespace LabelSongsAPI.Models.Relations
{
    public class SongCategory
    {
        public int IdSong { get; set; }
        public int IdCategory { get; set; }
        public Song Song { get; set; }
        public Category Category { get; set; }

    }
}
