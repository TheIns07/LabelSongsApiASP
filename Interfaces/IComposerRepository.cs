using LabelSongsAPI.Models;

namespace LabelSongsAPI.Interfaces
{
    public interface IComposerRepository
    {
        ICollection<Composer> GetComposers();
        Composer GetComposer(int id);
        ICollection<Composer> GetComposerOfSong(int IdSong);
        ICollection<Song> GetSongbyComposer(int IdComposer);
        bool HasComposerExists(int IdComposer);

        //Post Methods
        bool CreateComposer(Composer composer);
        bool UpdateComposer(Composer composer);
        bool DeleteComposer(Composer composer);
        bool Save();
        //Special Methods



    }
}
