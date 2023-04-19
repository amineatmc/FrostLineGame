using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using FrostLineGame.Models.Dto;
using FrostLineGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrostLineGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly Context _context;
        public DiscountController(Context context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<List<Discount>> GetList()
        {
            var values = _context.Discounts.Where(x => x.IsDeleted == false).ToList();
            return values;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add(Discount discount)
        {
            discount.IsDeleted = false;
            string name = discount.Name.ToUpper();
            discount.CreatedDate = DateTime.Now;
            discount.Name = name;
            _context.Add(discount);
            _context.SaveChanges();

            return Ok("eklendi");
        }

        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> Update(DiscountUpdateDto discount)
        {
            var values = _context.Discounts.Where(x => x.DiscountID==discount.DiscountID).First();
            if (values==null)
            {
                return BadRequest("Böyle Bir Kayır Bulunmamaktadır.");
            }
            if (discount.Name != null && discount.Name != "")
            {
                values.Name = discount.Name.ToUpper();
            }
            if (discount.DiscountRate != null && discount.DiscountRate != 0)
            {
                values.DiscountRate = discount.DiscountRate;
            }
            else
            {
                return BadRequest("Hatalı Girişler.Kontrol edip tekrar girin..");
            }
            _context.Update(values);
            _context.SaveChangesAsync();
            return Ok("Güncellendi");
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Discounts == null)
            {
                return NotFound();
            }
            var disc = await _context.Discounts.FindAsync(id);
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