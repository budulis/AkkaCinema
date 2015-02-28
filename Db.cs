using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaCinema
{
    class Db
    {
        public static IEnumerable<string> Users { get; set; }
        public static IEnumerable<string> Movies { get; set; }
        static Db()
        {
            Users = Enumerable.Empty<string>();
            Movies = Enumerable.Empty<string>();
        }
    }
}
