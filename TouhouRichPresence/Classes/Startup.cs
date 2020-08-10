using System.Threading.Tasks;

namespace TouhouRichPresence.Classes
{
    public class Startup
    {
        public Startup(string[] args)
        {
        }

        public async Task StartAsync()
        {
            using TouhouManager touhouManager = new TouhouManager();
            try
            {
                await touhouManager.StartAsync();
            }
            catch (TaskCanceledException) { }
        }
    }
}