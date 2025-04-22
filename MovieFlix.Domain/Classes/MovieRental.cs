namespace MovieFlix.Domain.Classes
{
    public class MovieRental
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}