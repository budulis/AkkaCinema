using System;

namespace AkkaCinema
{
    public class Commands
    {
        public class StartMovie
        {
            public string UserName { get; private set; }
            public string Title { get; private set; }
            public StartMovie(string userName,string title)
            {
                UserName = userName;
                Title = title;
            }
        }

        public class UnknownCommand
        {
            public object Data { get; private set; }

            public UnknownCommand(object data)
            {
                Data = data;
            }
        }
    }
}