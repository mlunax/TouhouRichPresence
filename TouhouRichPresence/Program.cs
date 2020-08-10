using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TouhouRichPresence.Classes;
using TouhouRichPresence.Touhou;

namespace TouhouRichPresence
{
    class Program
    {
        static Task Main(string[] args)
        {
            return new Startup(args).StartAsync();
        }
    }
}
