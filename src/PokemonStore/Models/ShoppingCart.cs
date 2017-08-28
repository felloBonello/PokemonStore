using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonStore.Models
{
    public class ShoppingCart
    { 
        public ShoppingCart()
        {
            CartItem = new HashSet<CartItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal Tax { get; set; }
        [Required]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }

        public virtual ICollection<CartItem> CartItem { get; set; }

    }
}
