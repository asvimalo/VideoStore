namespace VideoStore
{
    public class Rental
    {
        public string Movie { get;  set; }
        public string Customer { get; set; }
        public System.DateTime DayRented { get; set; }
        public System.DateTime DayReturn { get; set; }
    }
}