using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouhouRichPresence.Touhou;

namespace TouhouRichPresence.Classes
{
    public class Startup
    {
        public Startup(string[] args)
        {
        }

        public async Task StartAsync()
        {
            using var touhouManager = new TouhouManager();
            try
            {
                await touhouManager.StartAsync();
            }
            catch (TaskCanceledException) { }
        }
    }
}
