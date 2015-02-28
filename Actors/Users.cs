using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace AkkaCinema.Actors
{
    class Users : UntypedActor
    {
        private readonly List<KeyValuePair<string, ActorRef>> _users;

        public Users()
        {
            _users = new List<KeyValuePair<string, ActorRef>>();
        }

        protected override void OnReceive(object message)
        {
            ProcessCommand((dynamic)message);
        }

        private void ProcessCommand(Commands.StartMovie command)
        {
            UpdateUsers(command.UserName);
            Notify(command);
        }

        private void UpdateUsers(string userName)
        {
            if (_users.Any(x => x.Key == userName)) 
                return;
            
            var userRef = Context.ActorOf(Props.Create(()=> new User(userName)), userName);
            _users.Add(new KeyValuePair<string, ActorRef>(userName,userRef));
        }

        private void Notify(Commands.StartMovie command)
        {
            _users.Single(x=>x.Key == command.UserName).Value.Tell(command.Title);
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(e => Directive.Escalate);
        }
    }
}