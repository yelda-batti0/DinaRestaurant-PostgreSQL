
namespace DatabaseMastery.DinnerMenuPostgreSQL.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal AverageRating { get; set; } = 0;  // Trigger ile otomatik güncellenecek
        public List<Review> Reviews { get; set; }
    }
}
