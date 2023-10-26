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
    public class ComposerController : Controller
    {
        private readonly IComposerRepository _composerRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly ISongRepository _songRepository;

        public IMapper _mapper;

        public ComposerController(IComposerRepository composerRepository, IMapper mapper, ILabelRepository labelRepository, ISongRepository songRepository)
        {
            _composerRepository = composerRepository;
            _mapper = mapper;
            _labelRepository = labelRepository;
            _songRepository = songRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ComposerDTO>))]
        public IActionResult GetComposers()
        {
            var composers = _mapper.Map<List<ComposerDTO>>(_composerRepository.GetComposers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(composers);
        }

        [HttpGet("{IdComposer}")]
        [ProducesResponseType(200, Type = typeof(ComposerDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetComposerByID(int IdComposer)
        {
            if (!_composerRepository.HasComposerExists(IdComposer))
            {
                return NotFound();
            }
            var song = _mapper.Map<Composer>(_composerRepository.GetComposer(IdComposer));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(song);
        }

        [HttpGet("composer/{IdSong}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ComposerDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetComposerOfSong(int IdSong)
        {
            if (!_songRepository.SongExists(IdSong))
            {
                return NotFound();
            }

            var label = _mapper.Map<Composer>(
                    _composerRepository.GetComposerOfSong(IdSong));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(label);
        }

        [HttpGet("song/{IdComposer}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SongDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetSongbyComposer(int IdComposer)
        {
            if (!_composerRepository.HasComposerExists(IdComposer))
            {
                return NotFound();
            }

            var label = _mapper.Map<Song>(
                    _composerRepository.GetSongbyComposer(IdComposer));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(label);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateComposer([FromBody] ComposerDTO composer, [FromQuery] int LabelId)
        {
            if (composer == null)
            {
                return BadRequest(ModelState);
            }

            var existingComposer = _composerRepository.GetComposers()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == composer.Name.Trim().ToUpper());

            if (existingComposer != null)
            {
                ModelState.AddModelError("", "Composer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var composerMap = _mapper.Map<Composer>(composer);
            composerMap.Label = _labelRepository.GetLabelByID(LabelId);

            if (!_composerRepository.CreateComposer(composerMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the saving composer");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in saving composer!");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateComposer([FromBody] ComposerDTO composer, [FromQuery] int IdComposer)
        {
            if (composer == null)
            {
                return BadRequest(ModelState);
            }
            if (IdComposer != composer.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var composerMap = _mapper.Map<Composer>(composer);

            if (!_composerRepository.UpdateComposer(composerMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the updating composer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteComposer(int IdComposer)
        {
            if (!_composerRepository.HasComposerExists(IdComposer))
            {
                return NotFound();
            }

            var composerToDelete = _composerRepository.GetComposer(IdComposer);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_composerRepository.DeleteComposer(composerToDelete))
            {
                ModelState.AddModelError("", "Error al borrar la canción seleccionada");
            }

            return NoContent();
        }

    }
}
