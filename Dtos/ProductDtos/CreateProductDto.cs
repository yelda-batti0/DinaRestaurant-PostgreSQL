namespace DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ProductDtos
{
    public class CreateProductDto
    {
 
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
