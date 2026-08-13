namespace DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ReviewDtos
{
    public class CreateReviewDto
    {
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }        // 1-5 arası
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }       // Admin onayı

        // İlişki
        public int ProductId { get; set; }
    }
}
