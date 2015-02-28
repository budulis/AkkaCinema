using System;
using System.Diagnostics;
using Akka.Actor;

namespace AkkaCinema.Actors
{
    class CommandReceiver : UntypedActor
    {
        private readonly ActorRef _player;
        private readonly ActorRef _logger;
        public CommandReceiver(ActorRef player)
        {
            _player = player;
            _logger = Context.ActorOf(Props.Create<Logger>());
        }

        protected override void OnReceive(object message)
        {
            var command = message as string;

            if (String.IsNullOrWhiteSpace(command))
                _logger.Tell(new Logger.LogMessage("No command recieved",Logger.LogType.Error));
            else
            {
                var commandArgs = command.Split(';');

                if (commandArgs.Length != 2)
                    _logger.Tell(new Logger.LogMessage("Bad command", Logger.LogType.Error));
                else
                {
                    _player.Tell(new Commands.StartMovie(commandArgs[0], commandArgs[1]));
                    _logger.Tell(new Logger.LogMessage(command, Logger.LogType.Audit));
                }
            }
        }
    }
}