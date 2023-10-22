using LabelSongsAPI.Models;

namespace LabelSongsAPI.Interfaces
{
    public interface ILabelRepository
    {
        ICollection<Label> GetLabels();
        Label GetLabelByID(int id);
        Label GetLabelByComposer(int IdComposer);
        ICollection<Composer> GetComposersByLabel(int IdLabel);
        bool LabelExists(int id);

        //Post Methods
        bool CreateLabel(Label label);
        bool UpdateLabel(Label label);
        bool DeleteLabel(Label label);
        bool Save();

        //Special Methods


    }
}
