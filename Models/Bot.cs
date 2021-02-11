//описание бота

using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using tel_bot_net.Models.Commands;

namespace tel_bot_net.Models
{
    public static class Bot
    {
        private static TelegramBotClient botClient;
        private static List<Command> commandList = new List<Command>();
        public static IReadOnlyList<Command> Commands => commandList.AsReadOnly();

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }

            //комманды добавлять сдесь
            commandList.Add(new StartCommand());

            //
            botClient = new TelegramBotClient(AppSettings.Key);
            string hook = string.Format(AppSettings.Url, "/api/message/update");//ссылка на тунель Ngrock + ссылка на путь контроллера
            await botClient.SetWebhookAsync(hook);
            return botClient;
        }


    }
}
