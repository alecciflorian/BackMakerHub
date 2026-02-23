using BackMakerHub.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackMakerHub.Models
{
    public class StockLog
    {
        public int Id { get; set; }
        public DateOnly date { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; } = null;
        public int QuantityAdded { get; set; }
    }
}
