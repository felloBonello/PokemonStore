using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PokemonStore.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductId { get; set; }
        [Required]
        public int QtySold { get; set; }
        [Required]
        public int QtyOrdered { get; set; }
        [Required]
        public int QtyBackOrdered { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
