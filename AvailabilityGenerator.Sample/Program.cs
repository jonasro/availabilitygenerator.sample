// See https://aka.ms/new-console-template for more information

using Akka.Actor;
using AvailabilityGenerator.Sample.Actors;
using AvailabilityGenerator.Sample.Commands;

Console.WriteLine($"{DateTime.Now:hh:mm:ss} - Availability generator starting");

var actorSystem = ActorSystem.Create("availabilitygenerator");
var scheduler = actorSystem.ActorOf(Props.Create<AvailabilitySchedulerActor>(), "availabilityscheduler");

//Schedule can be read from config, database or hard coded
scheduler.Tell(new StartScheduler
{
    PackageRetrievalFrequency = new TimeSpan(0, 0, 0, 10),
    ItineraryRetrievalFrequency = new TimeSpan(0,0,0,5)
});

Console.ReadLine();
