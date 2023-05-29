using System.ComponentModel.DataAnnotations.Schema;

namespace EralpSoftTask.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public bool instock { get; set; }
        public int userid { get; set; }
    }
}
