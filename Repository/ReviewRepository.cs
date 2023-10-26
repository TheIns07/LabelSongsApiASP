using AutoMapper;
using LabelSongsAPI.Data;
using LabelSongsAPI.DTO;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;

namespace LabelSongsAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _datacontext;
        public IMapper _mapper;

        public ReviewRepository(DataContext datacontext, IMapper Mapper) 
        { 
            _datacontext = datacontext;
            _mapper = Mapper;
        }

        public bool CreateReview(Review review)
        {
            _datacontext.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _datacontext.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _datacontext.RemoveRange(reviews);
            return Save();
        }

        public ICollection<Review> GetAllReviewsOfSong(int IdSong)
        {
            return _datacontext.Reviews.Where(r => r.Song.ID == IdSong).ToList();
        }

        public Review GetReview(int IdReview)
        {
            return _datacontext.Reviews.Where(r => r.Id == IdReview).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _datacontext.Reviews.ToList();
        }

        public Review GetSongTrimToUpper(ReviewDTO reviewCreate)
        {
            throw new NotImplementedException();
        }

        public bool ReviewExists(int IdReview)
        {
            return _datacontext.Reviews.Any(r => r.Id == IdReview);
        }

        public bool Save()
        {
            var savedReview = _datacontext.SaveChanges();
            return savedReview >= 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _datacontext.Update(review);
            return Save();
        }
    }
}
