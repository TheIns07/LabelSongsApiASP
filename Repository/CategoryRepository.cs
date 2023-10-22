using LabelSongsAPI.Data;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;

namespace LabelSongsAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {   
        protected private readonly DataContext _datacontext;

        public CategoryRepository(DataContext context) { 
            _datacontext = context;
        }
        public bool CategoryExists(int id)
        {
           return _datacontext.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _datacontext.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _datacontext.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _datacontext.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return _datacontext.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Song> GetSongByCategory(int IdCategory)
        {
            return _datacontext.SongCategories.Where(c => c.IdCategory == IdCategory).Select(c => c.Song).ToList();
        }

        public bool Save()
        {
            var categorySong = _datacontext.SaveChanges();
            return categorySong >= 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _datacontext.Update(category);
            return Save();
        }
    }
}
