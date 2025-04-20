namespace MovieFlix.Domain.Classes
{
    public class Rental
    {
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime RentalExpiry { get; set; }
        public Decimal TotalCost { get; set; }


        public ICollection<Member> Members { get; set; }
        public IList<MovieRental> MovieRentals { get; set; }

    }
}