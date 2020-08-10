using System.Threading.Tasks;
using TouhouRichPresence.Classes;

namespace TouhouRichPresence
{
    internal class Program
    {
        private static Task Main(string[] args)
        {
            return new Startup(args).StartAsync();
        }
    }
}