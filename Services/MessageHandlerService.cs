using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using tel_bot_net.Models;

namespace tel_bot_net.Services
{
    public class MessageHandlerService
    {
        private static ReplyHandlerService replyHandler;
        private static DataBaseService dbSevice;

        public MessageHandlerService(ReplyHandlerService _replyHandler, DataBaseService _dbSevice)
        {
            replyHandler = _replyHandler;
            dbSevice = _dbSevice;
        }

        public async Task<bool> Handle(Update update)
        {

#if DEBUG
            Console.WriteLine("Message Query handled by service");
#endif

            if (update.Message == null) 
            {
#if DEBUG
                Console.WriteLine("Reply handled");
#endif
                return true;
            }
            

            var commands = Bot.Commands;
            var message = update.Message;
            var botClient = await Bot.GetBotClientAsync();

            if (replyHandler.Hold(update))
                return true;

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
#if DEBUG
                    Console.WriteLine($"Start execute commant: {command.Name}");
#endif
                    await command.Execute(message, botClient, replyHandler, dbSevice);
#if DEBUG
                    Console.WriteLine($"Stop execute commant: {command.Name}");
#endif
                    break;
                }
            }

            return true;
        }
    }
}
