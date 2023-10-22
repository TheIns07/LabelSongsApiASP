using LabelSongsAPI.Models;

namespace LabelSongsAPI.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetAllReviewsByReviewer(int IdReviewer);
        bool HasReviewers(int IdReviewer);

        //Post Methods
        bool ReviewerExists(int IdReviewer);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);

        //Special Methods
        bool Save();
    }
}
