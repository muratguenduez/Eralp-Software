namespace EralpSoftTask.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
