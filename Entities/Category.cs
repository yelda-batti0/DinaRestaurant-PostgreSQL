namespace DatabaseMastery.DinnerMenuPostgreSQL.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageUrl { get; set; }
        public bool CategoryStatus { get; set; }

        public List<Product> Products
        {
            get; set;
        }
    }
}
