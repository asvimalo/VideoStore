﻿namespace VideoStore
{
    public class Rental
    {
        public Movie Movie { get;  set; }
        public Customer Customer { get; set; }
        public System.DateTime DayRented { get; set; }
        public System.DateTime DayReturn { get; set; }
    }
}