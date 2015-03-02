using System;
using System.Collections.Generic;
using Akka.Actor;

namespace AkkaCinema.Actors
{
    class Supervisor : UntypedActor
    {
        private readonly IDictionary<string, int> _stats;
        public Supervisor()
        {
            _stats = new Dictionary<string, int>();
        }
        protected override void OnReceive(object message)
        {
            ProcessCommand((dynamic)message);
        }
        private void ProcessCommand(Commands.StartMovie command)
        {
            UpdateStats(command.Title);
            PrintStats();
        }
        private void UpdateStats(string title)
        {
            if (!_stats.ContainsKey(title))
                _stats.Add(title, 1);
            else
                _stats[title] = ++_stats[title];
        }
        private void PrintStats()
        {
            foreach (var stat in _stats)
                Console.WriteLine("Title: {0}-[{1}]", stat.Key, stat.Value.ToString("D"));
        }
    }
}