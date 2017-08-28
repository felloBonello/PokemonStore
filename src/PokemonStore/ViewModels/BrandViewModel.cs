using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using PokemonStore.Models;
namespace PokemonStore.ViewModels
{
    public class BrandViewModel
    {
        public BrandViewModel(){}
        public string BrandName { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public List<Brand> Brands { get; set; }
        public IEnumerable<SelectListItem> GetBrandNames()
        {
            return Brands.Select(Brand => new SelectListItem { Text = Brand.Name, Value = Brand.Name });
        }
    }
}
