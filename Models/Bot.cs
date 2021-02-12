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
            commandList.Add(new TestCommand());

            //
            botClient = new TelegramBotClient(AppSettings.Key);
            string hook = string.Format(AppSettings.Url, "/api/update");//ссылка на тунель Ngrock + ссылка на путь контроллера
            //string hook2 = string.Format(AppSettings.Url,)
            await botClient.SetWebhookAsync(hook);
            return botClient;
        }


    }
}
