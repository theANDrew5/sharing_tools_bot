//описание бота

using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using tel_bot_net.Models.Commands;


namespace tel_bot_net.Models
{
    public static class Bot
    {

        private static TelegramBotClient botClient;
        private static List<Commands.Command> commandList = new List<Commands.Command>();
        public static IReadOnlyList<Commands.Command> Commands => commandList.AsReadOnly();//список команд
        private static List<BotCommand> BotCommandList = new List<BotCommand>();

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }

            //комманды добавлять сдесь
            commandList.Add(new StartCommand());
            commandList.Add(new TestInlineButtonsCommand());
            commandList.Add(new TestReplies());
            commandList.Add(new CleanKeyboardCommand());

            //заполняем список команд для API
            foreach (var command in Commands)
            {
                var temp = new BotCommand();
                temp.Command = command.Name.Substring(1);
                temp.Description = command.Description;
                BotCommandList.Add(temp);
            }

            //создание бота
            botClient = new TelegramBotClient(AppSettings.Key);

            //настройка хука
            string hook = string.Format(AppSettings.Url, "/api/update");//ссылка на тунель Ngrock + ссылка на путь контроллера

            await botClient.SetWebhookAsync(hook,
                allowedUpdates: new[] { UpdateType.Message,UpdateType.CallbackQuery});

            //Настройка команд для API
            await botClient.SetMyCommandsAsync(BotCommandList); 

            return botClient;

        }


    }
} 
