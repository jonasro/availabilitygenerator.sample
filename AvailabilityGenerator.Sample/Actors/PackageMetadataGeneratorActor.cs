using Akka.Actor;
using AvailabilityGenerator.Sample.Commands;
using AvailabilityGenerator.Sample.Events;

namespace AvailabilityGenerator.Sample.Actors
{
    public class PackageMetadataGeneratorActor : ReceiveActor
    {
        public PackageMetadataGeneratorActor()
        {
            Receive<StartDataGeneration>(_ => Start());
        }

        private void Start()
        {
            //Retrieves data from SW / PG
            //Pushes data to Partner api endpoints

            Console.WriteLine($"{DateTime.Now:hh:mm:ss} - Retrieving packages");

            Thread.Sleep(2000);

            Console.WriteLine($"{DateTime.Now:hh:mm:ss} - Retrieved 1000 packages");
            Console.WriteLine($"{DateTime.Now:hh:mm:ss} - Dispatched 1000 packages to /api/flatfiles/packagemetadata");

            Context.Sender.Tell(new PackageRetrievalCompleted());
        }
    }
}
