using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Repositories.Interfaces;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository _repository;

        public CategoriesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            var categories = await _repository.GetAll().ToListAsync();

            return Ok(categories);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null || id < 1) return BadRequest();

            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();

            return Ok(category);

        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categoryDto)
        {
            await _repository.AddAsync(new Category { Name = categoryDto.Name });
            await _repository.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id < 1) return BadRequest();
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id == null || id < 1) return BadRequest();
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();
            _repository.Update(category);

            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
