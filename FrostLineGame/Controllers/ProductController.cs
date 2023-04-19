using FrostLineGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrostLineGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Context _context;


        public ProductController(Context context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<List<Product>> GetListRealPrice()
        {
            var values = _context.Products.Where(x => x.IsDeleted == false).ToList();
            return values;
        }

        [HttpGet("[action]")]
        public async Task<List<Product>> GetListByCategoryID(int id)
        {
            var values = _context.Products.Where(x => x.CategoryID == id).ToList();

            for (int i = 0; i < values.Count; i++)
            {
                var disc = _context.DiscountCategories.Where(x => x.CategoryID==values[i].CategoryID && x.IsDeleted==false).FirstOrDefault();
                if (disc!=null)
                {
                    if (disc.EndDate>DateTime.Now)
                    {
                        var discRate = _context.Discounts.Where(x => x.DiscountID==disc.DiscountID).FirstOrDefault();
                        var total = values[i].UnitPrice -(values[i].UnitPrice*discRate.DiscountRate/100);
                        values[i].UnitPrice = total;
                    }
                }
           
            }
            return values;
        }

        [HttpGet("[action]")]
        public async Task<Product> GetListByProductID(int id)
        {
            var values = _context.Products.Where(x => x.ProductID == id).First();
            var disc = _context.DiscountCategories.Where(x => x.CategoryID==values.CategoryID && x.IsDeleted==false).FirstOrDefault();
            if (disc!=null)
            {
                if (disc.EndDate>DateTime.Now)
                {
                    var discRate = _context.Discounts.Where(x => x.DiscountID==disc.DiscountID).FirstOrDefault();
                    var total = values.UnitPrice -(values.UnitPrice*discRate.DiscountRate/100);
                    values.UnitPrice = total;
                }
            }
            return values;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add(Product product)
        {
            product.IsDeleted = false;
            string name = product.Name.ToUpper();
            product.Name = name;
            _context.Add(product);
            _context.SaveChanges();

            return Ok("eklendi");
        }
    }
}
