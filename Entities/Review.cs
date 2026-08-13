namespace DatabaseMastery.DinnerMenuPostgreSQL.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }        // 1-5 arası
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }       // Admin onayı

        // İlişki
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
