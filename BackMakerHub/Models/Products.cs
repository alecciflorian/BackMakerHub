using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [NotMapped] //Entity Framework n'ira pas chercher cette colonne en DB
        [JsonPropertyName("lastStockInfo")]
        public string? LastStockInfo { get; set; }

        public decimal Price { get; set; }
        public List<StockLog> StockLogs { get; set; } = new();
        public CategoryClass Category { get; set; } = null!;
    }
}
