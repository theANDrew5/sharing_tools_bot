using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using tel_bot_net.Models.DbModels;

namespace tel_bot_net.Models.Commands
{
    public class LogIn : Command
    {
        public override string Name => "/login";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
             
        }
    }
}
