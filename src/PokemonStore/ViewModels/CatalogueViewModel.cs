using PokemonStore.Models;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace PokemonStore.ViewModels
{
    public class CatalogueViewModel
    {
        private List<Brand> _brands;
        [Required]
        public int Qty { get; set; }
        public string Id { get; set; }
        ///
        public int BrandId { get; set; }
        public IEnumerable<SelectListItem> GetBrands()
        {
            return _brands.Select(Brand => new SelectListItem
            {
                Text = Brand.Name,
                Value = Convert.ToString(Brand.Id)
            });
        }
        public void SetBrands(List<Brand> brands)
        {
            _brands = brands;
        }
    }
}