using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace tel_bot_net.Services
{
    public class CallbackHandlerService
    {

        public async Task<bool> Handle(Update update)
        {
#if DEBUG
            Console.WriteLine("Calback Query handled by sevice");
#endif
            return true;
        }
        
    }
}
