using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class ProductModel
    {
        public long Id { get; set; }
        public virtual UserModel Owner { get; set; }
        public virtual UserModel User { get; set; }
        [Required(ErrorMessage = "Required field"), MaxLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required field"), MaxLength(500)]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Required field"), MaxLength(4000)]
        public string LongDescription { get; set; }
        [Required(ErrorMessage = "Required field"), Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Required field"), Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "image")]
        public byte[] Picture1 { get; set; }
        [Column(TypeName = "image")]
        public byte[] Picture2 { get; set; }
        [Column(TypeName = "image")]
        public byte[] Picture3 { get; set; }
        [Required(ErrorMessage = "Required field")]
        public MyState State { get; set; }
    }
    public enum MyState
    {
        Available,
        ShoppingCart,
        Sold
    }
}
