using AutoMapper;
using LabelSongsAPI.Data;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;
using LabelSongsAPI.Models.Relations;

namespace LabelSongsAPI.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly DataContext _dataContext;
        public LabelRepository(DataContext dataContext) 
        { 
            _dataContext = dataContext;
        }

        public bool CreateLabel(Label label)
        {
            _dataContext.Add(label);
            return Save();
        }

        public bool DeleteLabel(Label label)
        {
            _dataContext.Remove(label);
            return Save();
        }

        public ICollection<Composer> GetComposersByLabel(int IdLabel)
        {
            return _dataContext.Composers.Where(c => c.Label.Id == IdLabel).ToList();
        }

        public Label GetLabelByComposer(int IdComposer)
        {
            return _dataContext.Composers.Where(o => o.Id == IdComposer).Select(c => c.Label).FirstOrDefault();
        }

        public Label GetLabelByID(int id)
        {
            return _dataContext.Label.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Label> GetLabels()
        {
            return _dataContext.Label.ToList();
        }

        public bool LabelExists(int IdLabel)
        {
            return _dataContext.Label.Any(c => c.Id == IdLabel);
        }

        public bool Save()
        {
            var labelSong = _dataContext.SaveChanges();
            return labelSong >= 0 ? true : false;
        }

        public bool UpdateLabel(Label label)
        {
            _dataContext.Update(label);
            return Save();
        }
    }
}
