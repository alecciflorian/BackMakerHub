namespace BackMakerHub.DTO_s
{
    public class ModifyProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
