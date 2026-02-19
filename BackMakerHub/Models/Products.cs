using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackMakerHub.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public string Type { get; set; } = null!;
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public CategoryClass Category { get; set; } = null!;
    }
}
