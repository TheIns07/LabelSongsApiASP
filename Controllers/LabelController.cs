using AutoMapper;
using LabelSongsAPI.Data;
using LabelSongsAPI.DTO;
using LabelSongsAPI.Interfaces;
using LabelSongsAPI.Models;
using LabelSongsAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LabelSongsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : Controller
    {
        public IMapper _mapper;
        protected readonly ILabelRepository _labelRepository;

        public LabelController(ILabelRepository labelRepository, IMapper imapper)
        {
            _labelRepository = labelRepository;
            _mapper = imapper; 
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LabelDTO>))]
        public IActionResult GetLabels()
        {
            var songs = _mapper.Map<List<LabelDTO>>(_labelRepository.GetLabels());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(songs);
        }

        [HttpGet("{IdLabel}")]
        [ProducesResponseType(200, Type = typeof(LabelDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetLabelByID(int IdLabel)
        {
            if (!_labelRepository.LabelExists(IdLabel))
            {
                return NotFound();
            }
            var song = _mapper.Map<LabelDTO>(_labelRepository.GetLabelByID(IdLabel));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(song);
        }

        [HttpGet("labelbycomposer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LabelDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetLabelByComposerID(int IdComposer)
        {
            var label = _mapper.Map<LabelDTO>(
                    _labelRepository.GetLabelByComposer(IdComposer));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            return Ok(label);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLabel([FromBody] LabelDTO label)
        {
            if (label == null)
            {
                return BadRequest(ModelState);
            }

            var existingLabel = _labelRepository.GetLabels()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == label.Name.Trim().ToUpper());

            if (existingLabel != null)
            {
                ModelState.AddModelError("", "Label already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var labelMap = _mapper.Map<Label>(label);



            if (!_labelRepository.CreateLabel(labelMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the saving the label");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in saving label!");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory([FromBody] LabelDTO label, [FromQuery] int IdLabel)
        {
            if (label == null)
            {
                return BadRequest(ModelState);
            }
            if (IdLabel != label.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var labelMap = _mapper.Map<Label>(label);

            if (!_labelRepository.UpdateLabel(labelMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the updating Category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLabel(int IdLabel)
        {
            if (!_labelRepository.LabelExists(IdLabel))
            {
                return NotFound();
            }

            var labelToDelete = _labelRepository.GetLabelByID(IdLabel);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_labelRepository.DeleteLabel(labelToDelete))
            {
                ModelState.AddModelError("", "Error al borrar la canción seleccionada");
            }

            return NoContent();
        }
    }
}
