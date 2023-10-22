using LabelSongsAPI.Models;

namespace LabelSongsAPI.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Song> GetSongByCategory(int IdCategory);
        bool CategoryExists(int id);

        //Post Methods
        bool CreateCategory(Category category);

        //Update methods
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
        //Special Methods
    }
}
