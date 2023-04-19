using FrostLineGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrostLineGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Context _context;
        public CategoryController(Context context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<List<Category>> GetList()
        {
            var values = _context.Categories.Where(x => x.IsDeleted == false).ToList();
            return values;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add(Category category)
        {
            category.IsDeleted = false;
            string name = category.Name.ToUpper();
            category.Name = name;
            _context.Add(category);
            _context.SaveChanges();

            return Ok("eklendi");
        }

    }
}
