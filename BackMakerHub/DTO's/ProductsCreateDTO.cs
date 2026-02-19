namespace BackMakerHub.DTO_s
{
    public class ProductsCreateDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; } = null!;
        public string CategoryName { get; set; } = string.Empty!;
    }
}
