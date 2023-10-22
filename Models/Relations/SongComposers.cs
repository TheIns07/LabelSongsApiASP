namespace LabelSongsAPI.Models.Relations
{
    public class SongComposer
    {
        public int IdSong { get; set; }
        public int IdComposer { get; set; }
        public Song Song { get; set; }
        public Composer Composer { get; set; }
    }
}
