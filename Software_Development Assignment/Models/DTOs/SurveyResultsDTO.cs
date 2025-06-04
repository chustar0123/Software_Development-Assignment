namespace Software_Development_Assignment.Models.DTOs
{
    public class SurveyResultsDTO
    {
        public int TotalSurveys { get; set; }
        public double AverageAge { get; set; }
        public int OldestAge { get; set; }
        public int YoungestAge { get; set; }

        public double PercentagePizza { get; set; }
        public double PercentagePasta { get; set; }
        public double PercentagePapWors { get; set; }

        public double AverageWatchMovies { get; set; }
        public double AverageListenRadio { get; set; }
        public double AverageEatOut { get; set; }
        public double AverageWatchTV { get; set; }
    }
}
