using System.Collections.Generic;

namespace PokemonStore.ViewModels
{
    public class ShoppingCartViewModel
    {
        public string Id { get; set; }
        public int CartItemId { get; set; }
        public string Product { get; set; }
        public decimal MSRP { get; set; }
        public int QtyS { get; set; }
        public int QtyO { get; set; }
        public int QtyB { get; set; }
        public decimal Extended { get; set; }
        public string UserId { get; set; }
        public string DateCreated { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalPrice { get; set; }
               
    }
}