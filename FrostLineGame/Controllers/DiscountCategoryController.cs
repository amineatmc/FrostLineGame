using FrostLineGame.Models.Dto;
using FrostLineGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrostLineGame.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCategoryController : ControllerBase
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DiscountCategoryController(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor =httpContextAccessor;
            var ss = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet("[action]")]
        public async Task<List<DiscountCategory>> GetList()
        {
            var values = _context.DiscountCategories.Where(x => x.IsDeleted == false).ToList();
            return values;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add(DiscountCategory discountCategory)
        {

            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            string decode = header[5].Value;
            string decodeToken = decode.Substring(7);
            try
            {
                var jwtToken = new JwtSecurityToken(decodeToken);
                var token = jwtToken.Payload.Claims.ToList();
                string userType = token[4].Value;
            }
            catch (Exception)
            {
                return BadRequest("Yetkisiz İşlem.");
            }

            var values = _context.DiscountCategories.Where(x => x.CategoryID==discountCategory.CategoryID && x.IsDeleted==false).FirstOrDefault();
            if (values!=null)
            {
                return BadRequest("Bu Kategori Mevcut bir İndirim Grubuna Dahil.");
            }
            discountCategory.IsDeleted = false;

            _context.Add(discountCategory);
            _context.SaveChanges();

            return Ok("Eklendi");
        }


        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> Update(DiscountCategory discountCategory)
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            string decode = header[5].Value;
            string decodeToken = decode.Substring(7);
            try
            {
                var jwtToken = new JwtSecurityToken(decodeToken);
                var token = jwtToken.Payload.Claims.ToList();
                string userType = token[4].Value;
            }
            catch (Exception)
            {
                return BadRequest("Yetkisiz İşlem.");
            }

            var values = _context.DiscountCategories.Where(x => x.DiscountCategoryID==discountCategory.DiscountCategoryID).First();
            if (values==null)
            {
                return BadRequest("Böyle Bir Kayıt Bulunmamaktadır.");
            }
            values.StartDate=discountCategory.StartDate;
            values.EndDate=discountCategory.EndDate;
            _context.Update(values);
            _context.SaveChangesAsync();
            return Ok("Güncellendi");
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers.ToList();
            string decode = header[5].Value;
            string decodeToken = decode.Substring(7);
            try
            {
                var jwtToken = new JwtSecurityToken(decodeToken);
                var token = jwtToken.Payload.Claims.ToList();
                string userType = token[4].Value;
            }
            catch (Exception)
            {
                return BadRequest("Yetkisiz İşlem.");
            }

            if (_context.DiscountCategories == null)
            {
                return NotFound();
            }
            var disc = await _context.DiscountCategories.FindAsync(id);
            if (disc== null)
            {
                return NotFound();
            }
            disc.IsDeleted= true;
            _context.Update(disc);
            _context.SaveChangesAsync();
            return Ok("Kayıt Silindi");
        }
    }
}
