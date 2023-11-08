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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public IMapper _mapper;

        public CategoryController(ICategoryRepository CategoryRepository, IMapper mapper)
        {
            _categoryRepository = CategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDTO>))]
        public IActionResult GetCategories()
        {
            var songs = _mapper.Map<List<CategoryDTO>>(_categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(songs);
        }

        [HttpGet("{IdCategory}")]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryByID(int IdCategory)
        {
            if (!_categoryRepository.CategoryExists(IdCategory))
            {
                return NotFound();
            }
            var song = _mapper.Map<CategoryDTO>(_categoryRepository.GetCategory(IdCategory));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(song);
        }

        [HttpGet("song/{IdCategory}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SongDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetSongByCategoryID(int IdCategory) {

            var songs = _mapper.Map<List<SongDTO>>(_categoryRepository.GetSongByCategory(IdCategory));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(songs);
            
        }

        [HttpPost, Authorize(Roles = "LabelOwner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDTO category)
        {
            if (category == null)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = _categoryRepository.GetCategories()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == category.Name.Trim().ToUpper());

            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(category);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the saving Category");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in Category song!");
        }

        [HttpPut, Authorize(Roles = "LabelOwner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory([FromBody] CategoryDTO category, [FromQuery] int IdCategory)
        {
            if (category == null)
            {
                return BadRequest(ModelState);
            }
            if (IdCategory != category.Id)
            {
                 return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(category);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something goes wrong in the updating Category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete, Authorize(Roles = "LabelOwner")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int IdCategory)
        {
            if (!_categoryRepository.CategoryExists(IdCategory))
            {
                return NotFound();
            }

            var categoryToDelete = _categoryRepository.GetCategory(IdCategory);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
