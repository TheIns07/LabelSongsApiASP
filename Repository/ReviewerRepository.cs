using AutoMapper;
using LabelSongsAPI.Data;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LabelSongsAPI.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {

        protected readonly DataContext _dataContext;
        public IMapper _imapper;
        public ReviewerRepository(DataContext dataContext, IMapper imapper) 
        {
            _dataContext = dataContext;
            _imapper = imapper;
        
        }
        public bool CreateReviewer(Reviewer reviewer)
        {
            _dataContext.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _dataContext.Remove(reviewer);
            return Save();
        }

        public ICollection<Review> GetAllReviewsByReviewer(int IdReviewer)
        {
            return _dataContext.Reviews.Where(r => r.Reviewer.Id == IdReviewer).ToList();
        }

        public Reviewer GetReviewer(int id)
        {
            return _dataContext.Reviewers.Where(r => r.Id == id).Include(e => e.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _dataContext.Reviewers.ToList();
        }

        public bool HasReviewers(int IdReviewer)
        {
            return _dataContext.Reviewers.Any(r => r.Id == IdReviewer);
        }

        public bool ReviewerExists(int IdReviewer)
        {
            return _dataContext.Reviewers.Any(p => p.Id == IdReviewer);
        }

        public bool Save()
        {
            var savedReviewer = _dataContext.SaveChanges();
            return savedReviewer >= 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
           _dataContext.Add(reviewer);
            return Save();
        }
    }
}
