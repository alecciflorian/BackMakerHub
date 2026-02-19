using System.Text.Json.Serialization;

namespace BackMakerHub.Models
{
    public class CategoryClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public List<Products> Products { get; set; } = null!;
    }
}
