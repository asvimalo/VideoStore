namespace Video_Store
{
    public class Rental
    {
        public string Movie { get;  set; }
        public string Customer { get; set; }
        public System.DateTime DayRented { get; set; }
        public System.DateTime DayReturn => DayRented.AddDays(3);
        
    }
}