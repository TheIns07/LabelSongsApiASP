using LabelSongsAPI.DTO;
using LabelSongsAPI.Models;

namespace LabelSongsAPI.Interfaces
{
    public interface ISongRepository
    {
        //Get methods
        ICollection<Song> GetAll();
        Song GetSongById(int id);
        Song GetSongByName(string name);
        Song GetSongByCategory(int IdCategory);

        //Post Methods
        bool CreateSong(Song song);
        bool CreateFullSong(int IdCategory, int IdComposer, Song song);
        bool UpdateSong(Song song); 
        bool DeleteSong(Song song);
        bool Save();

        //Special Methods
        public Song GetSongTrimToUpper(SongDTO songCreate);
        bool SongExists(int id);


    }
}
