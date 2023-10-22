using LabelSongsAPI.DTO;
using LabelSongsAPI.Models;

namespace LabelSongsAPI.Interfaces
{
    public interface IReviewRepository
    {        
        //Get methods
        ICollection<Review> GetReviews();
        Review GetReview(int id);   
        ICollection<Review> GetAllReviewsOfSong(int IdSong);
        bool ReviewExists(int IdReview);

        //Post Methods
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        
        //Special Methods
        public Review GetSongTrimToUpper(ReviewDTO reviewCreate);
        bool Save();



    }
}
