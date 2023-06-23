using Akka.Actor;
using AvailabilityGenerator.Sample.Commands;

namespace AvailabilityGenerator.Sample.Actors
{
    public class AvailabilitySchedulerActor : ReceiveActor
    {
        private IActorRef _packageMetadataActor;
        private IActorRef _itineraryActor;
        private TimeSpan _packageRetrievalFrequency;
        private TimeSpan _itineraryRetrievalFrequency;

        //This is responsible for starting working actors and scheduling next runs 

        public AvailabilitySchedulerActor()
        {
            Receive<StartScheduler>(Start);
            Receive<PackageRetrievalCompleted>(_ => ScheduleNextPackageMetadata());
            Receive<ItineraryRetrievalCompleted>(_ => ScheduleNextItinerary());
        }

        private void Start(StartScheduler startCommand)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss} - Started scheduler");

            //Set schedule
            _packageRetrievalFrequency = startCommand.PackageRetrievalFrequency;
            _itineraryRetrievalFrequency = startCommand.ItineraryRetrievalFrequency;

            //Create worker actors
            _packageMetadataActor = Context.ActorOf(Props.Create<PackageMetadataGeneratorActor>(), "packagemetadata");
            _itineraryActor = Context.ActorOf(Props.Create<ItineraryGeneratorActor>(), "itinerary");
            
            //Initiate data generation
            _packageMetadataActor.Tell(new StartDataGeneration());
            _itineraryActor.Tell(new StartDataGeneration());
        }

        private void ScheduleNextPackageMetadata()
        {
            Context.System.Scheduler.ScheduleTellOnce(_packageRetrievalFrequency, _packageMetadataActor, new StartDataGeneration(), Self);
        }

        private void ScheduleNextItinerary()
        {
            Context.System.Scheduler.ScheduleTellOnce(_itineraryRetrievalFrequency, _itineraryActor, new StartDataGeneration(), Self);
        }
    }
}
