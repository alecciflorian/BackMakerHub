using BackMakerHub.Models;
namespace BackMakerHub.Models
{
    public class StockLog
    {
        public int Id { get; set; }
        public DateOnly date { get; set; }
        public int ProductId { get; set; }
    }
}
