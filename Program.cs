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
            var player = actorSystem.ActorOf(Props.Create<Player>(), "Player");
            var command = "";

            Console.WriteLine("Enter: USERNAME;MOVIE TITLE");

            while ((command = Console.ReadLine()) != "exit")
            {
                if (command != null)
                {
                    var commandArgs = command.Split(';');
                    player.Tell(new Commands.StartMovie(commandArgs[0], commandArgs[1]));
                }
                else
                    player.Tell(new Commands.UnknownCommand(command));

            }

            actorSystem.Shutdown();
            Console.WriteLine("Player shut down");
        }
    }
}
