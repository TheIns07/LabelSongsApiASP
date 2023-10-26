using AutoMapper;
using LabelSongsAPI.DTO;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;
using LabelSongsAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LabelSongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {
        public IMapper _mapper;
        protected readonly IReviewRepository _reviewRepository;
        protected readonly ISongRepository _songRepository;
        protected readonly IReviewerRepository _reviewerRepository;


        public ReviewsController(IReviewRepository reviewRepository, IMapper imapper, ISongRepository songRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = imapper;
            _songRepository = songRepository;
            _reviewerRepository = 
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        public IActionResult GetReviews()
        {
            var songs = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(songs);
        }

        [HttpGet("getById/{IdReview}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewByID(int IdReview)
        {
            if (!_reviewRepository.ReviewExists(IdReview))
            {
                return NotFound();
            }
            var song = _mapper.Map<ReviewDTO>(_reviewRepository.GetReview(IdReview));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(song);
        }

        [HttpGet("{IdSong}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllReviewsOfSong(int IdSong)
        {
            var label = _mapper.Map<ReviewDTO>(
                    _reviewRepository.GetAllReviewsOfSong(IdSong));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(label);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody] ReviewDTO review, [FromQuery] int IdReviewer, [FromQuery] int IdSong)
        {
            if (review == null)
            {
                return BadRequest(ModelState);
            }

            var existingSong = _reviewRepository.GetReviews()
                .FirstOrDefault(c => c.ReviewTitle.Trim().ToUpper() == review.ReviewTitle.Trim().ToUpper());

            if (existingSong != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(review);

            reviewMap.Song = _songRepository.GetSongById(IdSong);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(IdReviewer);

            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the saving the review");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in saving review!");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview([FromBody] ReviewDTO review, [FromQuery] int IdReview)
        {
            if (review == null)
            {
                return BadRequest(ModelState);
            }
            if (IdReview != review.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(review);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int IdReview)
        {
            if (!_reviewRepository.ReviewExists(IdReview))
            {
                return NotFound();
            }

            var reviewToDelete = _reviewRepository.GetReview(IdReview);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
