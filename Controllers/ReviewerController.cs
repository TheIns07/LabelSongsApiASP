using AutoMapper;
using LabelSongsAPI.DTO;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;
using LabelSongsAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabelSongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        public IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDTO>))]
        public IActionResult GetReviewers()
        {
            var songs = _mapper.Map<List<ReviewerDTO>>(_reviewerRepository.GetReviewers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(songs);
        }

        [HttpGet("{IdReviewer}")]
        [ProducesResponseType(200, Type = typeof(ReviewerDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerByID(int IdReviewer)
        {
            if (!_reviewerRepository.HasReviewers(IdReviewer))
            {
                return NotFound();
            }
            var reviewer = _mapper.Map<ReviewerDTO>(_reviewerRepository.GetReviewer(IdReviewer));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviewer);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDTO reviewer)
        {
            if (reviewer == null)
            {
                return BadRequest(ModelState);
            }

            var existingReviewer = _reviewerRepository.GetReviewers()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == reviewer.Name.Trim().ToUpper());

            if (existingReviewer != null)
            {
                ModelState.AddModelError("", "Song already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewerMap = _mapper.Map<Reviewer>(reviewer);



            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the saving song");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in saving reviewer!");
        }

        [HttpPut, Authorize(Roles = "Reviewer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer([FromBody] ReviewerDTO reviewer, [FromQuery] int IdReviewer)
        {
            if (reviewer == null)
            {
                return BadRequest(ModelState);
            }
            if (IdReviewer != reviewer.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewerMap = _mapper.Map<Reviewer>(reviewer);

            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete, Authorize(Roles = "Reviewer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int IdReviewer)
        {
            if (!_reviewerRepository.ReviewerExists(IdReviewer))
            {
                return NotFound();
            }

            var reviewerToDelete = _reviewerRepository.GetReviewer(IdReviewer);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "Error al borrar la canción seleccionada");
            }

            return NoContent();
        }

    }
}
