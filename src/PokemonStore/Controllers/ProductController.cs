using Microsoft.AspNet.Mvc;
using PokemonStore.Models;
using PokemonStore.ViewModels;

namespace PokemonStore.Controllers
{
    public class ProductController : Controller {
        AppDbContext _db;
        public ProductController(AppDbContext context)
        {
            _db = context;
        }
        // GET: /<controller>/         
        public IActionResult Index(BrandViewModel Brand)
        {
            ProductModel model = new ProductModel(_db);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.BrandName = Brand.BrandName;
            viewModel.Product = model.GetAllByBrandName(Brand.BrandName);
            return View(viewModel);
        }
    }
}