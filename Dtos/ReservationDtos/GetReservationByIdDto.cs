namespace DatabaseMastery.DinnerMenuPostgreSQL.Dtos.ReservationDtos
{
    public class GetReservationByIdDto
    {
        public int ReservationId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public int GuestCount { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
