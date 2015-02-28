using System;
using Akka.Actor;
using Microsoft.CSharp.RuntimeBinder;

namespace AkkaCinema.Actors
{
    class Logger : UntypedActor
    {
        public class LogMessage
        {
            public object Content { get; private set; }
            public LogType Type { get; private set; }

            public LogMessage(object content, LogType type)
            {
                Content = content;
                Type = type;
            }
        }
        public enum LogType
        {
            Error, Warning, Audit
        }

        protected override void OnReceive(object message)
        {
            ProcessCommand((dynamic)message);
        }

        private void ProcessCommand(LogMessage message)
        {
            switch (message.Type)
            {
                case LogType.Error:
                    LogError(message.Content);
                    break;
                case LogType.Warning:
                    LogWarning(message.Content);
                    break;
                default:
                    LogAudit(message.Content);
                    break;
            }
        }

        private void LogError(object content)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine(content);
            Console.ForegroundColor = current;
        }

        private void LogWarning(object content)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(content);
            Console.ForegroundColor = current;
        }

        private void LogAudit(object content)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(content);
            Console.ForegroundColor = current;
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(e =>
            {
                if (e is RuntimeBinderException)
                    return Directive.Resume;
                return Directive.Stop;
            });
        }
    }
}