using AutoMapper;
using LabelSongsAPI.DTO;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;
using LabelSongsAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LabelSongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly IReviewRepository _reviewRepository;
        public IMapper _mapper;

        public SongController(ISongRepository SongRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            _songRepository = SongRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SongDTO>))]
        public IActionResult GetSongs()
        {
            var songs = _mapper.Map <List<SongDTO>> (_songRepository.GetAll());   
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(songs);
        }

        [HttpGet("{IdSong}")]
        [ProducesResponseType(200, Type = typeof(SongDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetSongByID(int IdSong) 
        {
            if (!_songRepository.SongExists(IdSong))
            {
                return NotFound();
            }
            var song = _mapper.Map <SongDTO> ( _songRepository.GetSongById(IdSong));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(song);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(200, Type = typeof(SongDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetSongByName(string name)
        {
            var song = _mapper.Map <SongDTO> (_songRepository.GetSongByName(name));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(song);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSong([FromBody] SongDTO song)
        {
            if (song == null)
            {
                return BadRequest(ModelState);
            }

            var existingSong = _songRepository.GetAll()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == song.Name.Trim().ToUpper());

            if (existingSong != null)
            {
                ModelState.AddModelError("", "Song already exists");    
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var songMap = _mapper.Map<Song>(song);



            if (!_songRepository.CreateSong(songMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the saving song");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in saving song!");
        }

            [HttpPost("createFull")]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            public IActionResult CreateSongFull([FromQuery] int IdComposer, [FromQuery] int IdCategory, [FromBody] SongDTO songCreated)
            {
                if (songCreated == null)
                {
                    return BadRequest(ModelState);
                }

                var existingSong = _songRepository.GetSongTrimToUpper(songCreated);

                if (existingSong != null)
                {
                    ModelState.AddModelError("", "Song already exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var songMap = _mapper.Map<Song>(songCreated);

                if (!_songRepository.CreateFullSong(IdCategory, IdComposer, songMap))
                {
                    ModelState.AddModelError("", "Something goes wrong in the saving song");
                    return StatusCode(500, ModelState);
                }

                return Ok("Success in saving song!");
            }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSong([FromBody] SongDTO song, [FromQuery] int IdSong)
        {
            if (song == null)
            {
                return BadRequest(ModelState);
            }
            if (IdSong != song.ID)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var songMap = _mapper.Map<Song>(song);

            if (!_songRepository.UpdateSong(songMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the updating song");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSong(int IdSong)
        {
            if (!_songRepository.SongExists(IdSong))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetAllReviewsOfSong(IdSong);
            var songToDelete = _songRepository.GetSongById(IdSong);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_songRepository.DeleteSong(songToDelete))
            {
                ModelState.AddModelError("", "Error al borrar la canción seleccionada");
            }

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            return NoContent();
        }


    }
}
