using System;
using Akka.Actor;

namespace AkkaCinema.Actors
{
    class User : UntypedActor
    {
        private readonly string _name;
        private string _movieTitle;

        public User(string name)
        {
            _name = name;
        }

        protected override void OnReceive(object message)
        {
            var command = message as string;
            if (command != null)
            {
                _movieTitle = command;
                Console.WriteLine("{0} is watching {1}", _name, _movieTitle);
            }
            else
                throw new NotSupportedException("Title can not be null");
        }
    }
}