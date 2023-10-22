using LabelSongsAPI.Data;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;

namespace LabelSongsAPI.Repository
{
    public class ComposerRepository : IComposerRepository
    {
        private readonly DataContext _dataContext;
        public ComposerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateComposer(Composer composer)
        {
            _dataContext.Add(composer);
            return Save();
        }

        public bool DeleteComposer(Composer composer)
        {
            _dataContext.Remove(composer);
            return Save();
        }

        public Composer GetComposer(int id)
        {
            return _dataContext.Composers.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Composer> GetComposerOfSong(int IdSong)
        {
            return _dataContext.SongComposers.Where(so => so.Song.ID == IdSong).Select(c => c.Composer).ToList();
        }

        public ICollection<Composer> GetComposers()
        {
            return _dataContext.Composers.ToList();
        }

        public ICollection<Song> GetSongbyComposer(int IdComposer)
        {
            return _dataContext.SongComposers.Where(c => c.IdComposer == IdComposer).Select(s => s.Song).ToList();
        }

        public bool HasComposerExists(int IdComposer)
        {
            return _dataContext.Composers.Any(c => c.Id == IdComposer);
        }

        public bool Save()
        {
            var composerSaved = _dataContext.SaveChanges();
            return composerSaved >= 0 ? true : false; 
        }

        public bool UpdateComposer(Composer composer)
        {
            _dataContext.Update(composer);
            return Save();
        }
    }
}
