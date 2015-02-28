using System;
using Akka.Actor;
using Microsoft.CSharp.RuntimeBinder;

namespace AkkaCinema.Actors
{
    class Player : UntypedActor
    {
        private readonly ActorRef _users;
        private readonly ActorRef _supervisor;

        public Player()
        {
            _users = Context.ActorOf(Props.Create<Users>(), "Users");
            _supervisor = Context.ActorOf(Props.Create<Supervisor>(), "Supervisor");
        }

        protected override void OnReceive(object message)
        {
            ProcessCommand((dynamic)message);
        }

        private void ProcessCommand(Commands.StartMovie command)
        {
            _users.Tell(command);
            _supervisor.Tell(command);
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(e =>
            {
                if (e is RuntimeBinderException)
                {
                    _supervisor.Tell(new Commands.UnknownCommand(e));
                    return Directive.Resume;
                }
                if (e is NotSupportedException)
                {
                    _supervisor.Tell(e.Message);
                    return Directive.Resume;
                }

                _supervisor.Tell(e);
                return Directive.Stop;
            });
        }
    }
}
