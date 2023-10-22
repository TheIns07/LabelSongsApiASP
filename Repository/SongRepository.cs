using LabelSongsAPI.Data;
using LabelSongsAPI.DTO;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;
using LabelSongsAPI.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace LabelSongsAPI.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly DataContext _dataContext;
        public SongRepository(DataContext datacontext) {
            _dataContext = datacontext; 
        }

        public bool CreateFullSong(int IdCategory, int IdComposer, Song song)
        {
            var songComposerData = _dataContext.Composers.FirstOrDefault(a => a.Id == IdComposer);
            var category = _dataContext.Categories.FirstOrDefault(a => a.Id == IdCategory);

            if (songComposerData == null || category == null)
            {
                return false;
            }

            var songComposer = new SongComposer()
            {
                Composer = songComposerData,
                Song = song
            };
            _dataContext.Add(song);
            _dataContext.Add(songComposer);

            var songCategory = new SongCategory()
            {
                Category = category,
                Song = song,
            };
            _dataContext.Add(songCategory);
            

            return Save(); 
        }


        public bool CreateSong(Song song)
        {
            _dataContext.Add(song);

            return Save();
        }

        public ICollection<Song> GetAll()
        {
            return _dataContext.Songs.OrderBy(s => s.ID).ToList();
        }

        public Song GetSongById(int id)
        {
            return _dataContext.Songs.Where(p => p.ID == id).FirstOrDefault();
        }

        public Song GetSongByName(string name)
        {
            return _dataContext.Songs.Where(p => p.Name == name).FirstOrDefault();
        }

        public bool Save()
        {
            var savedSong = _dataContext.SaveChanges();
            return savedSong >= 0 ? true : false;
        }

        public bool SongExists(int id)
        {
            return _dataContext.Songs.Any(p => p.ID == id);
        }

        public Song GetSongTrimToUpper(SongDTO songCreate)
        {
            return GetAll().Where(c => c.Name.Trim().ToUpper() == songCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool UpdateSong(Song song)
        {
            _dataContext.Update(song); 
            return Save();
        }

        public bool DeleteSong(Song song)
        {
           var songFind = _dataContext.Songs.FirstOrDefault(p => p.ID == song.ID);
            if (song != null)
            {
                return false;
            }
            _dataContext.Remove(song);
            return Save();
        }

        public Song GetSongByCategory(int IdCategory)
        {
            throw new NotImplementedException();
        }
    }
}
