using Akka.Actor;
using Akka.Delivery;
using AvailabilityGenerator.Sample.Commands;
using AvailabilityGenerator.Sample.Events;

namespace AvailabilityGenerator.Sample.Actors
{
    public class ItineraryGeneratorActor : ReceiveActor
    {
        //This can be broken up into retrieval and sender actors as well if we want. We get some error handling etc... out of the box with akka
        //With this pattern you can easily have one actor pr "file" working independently and even splitting retrieval and sending out into separate actors since retrieval does not neccesarily need to wait until sending is completed

        public ItineraryGeneratorActor()
        {
            Receive<StartDataGeneration>(_ => Start());
        }

        private void Start()
        {
            //Retrieves data from SW / PG
            //Pushes data to Partner api endpoints

            Console.WriteLine($"{DateTime.Now:hh:mm:ss} - Retrieving itinerary");

            Thread.Sleep(2000);

            Console.WriteLine($"{DateTime.Now:hh:mm:ss} - Retrieved 524 itineraries");
            Console.WriteLine($"{DateTime.Now:hh:mm:ss} - Dispatched 524 itineraries to /api/flatfiles/itinerary");

            Context.Sender.Tell(new ItineraryRetrievalCompleted());
        }
    }
}
