using PokemonStore.Models;
using System.Collections.Generic;
using System;

namespace PokemonStore.ViewModels
{
    public class ProductViewModel
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int ATTACK { get; set; }
        public int DEFENCE { get; set; }
        public int SPECIALATTACK { get; set; }
        public int SPECIALDEFENCE { get; set; }
        public int SPEED { get; set; }
        public int HP { get; set; }
        public string Brand { get; set; }
        public string GraphicName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal MSRP { get; set; }
        public int QtyOnHand { get; set; }
        public int QtyOnBackOrder { get; set; }
        public string Description { get; set; }
        public string JsonData { get; set; }
    }
}