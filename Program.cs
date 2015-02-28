using System;
using Akka.Actor;
using AkkaCinema.Actors;

namespace AkkaCinema
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("Cinema");

            var player = actorSystem.ActorOf(Props.Create<Player>());

            var receiver = actorSystem.ActorOf(Props.Create(()=> new CommandReceiver(player)), "Player");
            var command = "";

            Console.WriteLine("Enter: USERNAME;MOVIE TITLE");

            while ((command = Console.ReadLine()) != "exit")
            {
                receiver.Tell(command);
            }

            actorSystem.Shutdown();
            Console.WriteLine("Player shut down");
        }
    }
}
