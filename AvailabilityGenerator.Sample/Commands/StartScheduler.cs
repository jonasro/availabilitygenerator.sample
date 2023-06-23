namespace AvailabilityGenerator.Sample.Commands
{
    public class StartScheduler
    {
        public TimeSpan PackageRetrievalFrequency { get; set; }
        public TimeSpan ItineraryRetrievalFrequency { get; set; }
    }
}
