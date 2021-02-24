//описание бота

using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using tel_bot_net.Models.Commands;
using tel_bot_net.Models.Callbacks;


namespace tel_bot_net.Models
{
    public static class Bot
    {

        private static TelegramBotClient botClient;
        private static List<Commands.Command> commandList = new List<Commands.Command>()
        {
            //комманды добавлять сдесь
            new StartCommand(),
            new TestInlineButtonsCommand(),
            new TestRepliesCommand(),
            new CleanKeyboardCommand()
        };
        public static IReadOnlyList<Commands.Command> commands => commandList.AsReadOnly();//список команд
        private static List<BotCommand> botCommandList = new List<BotCommand>();

        private static List<Callback> callbackList = new List<Callback>() 
        {
            //функуии добавлять сдесь
            new ToolShareCallback()
        };
        public static IReadOnlyList<Callback> callbacks => callbackList.AsReadOnly();//список колбеков


        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }

            //заполняем список команд для API
            foreach (var command in commands)
            {
                var temp = new BotCommand();
                temp.Command = command.Name.Substring(1);
                temp.Description = command.Description;
                botCommandList.Add(temp);
            }

            //создание бота
            botClient = new TelegramBotClient(AppSettings.Key);

            //настройка хука
            string hook = string.Format(AppSettings.Url, "/api/update");//ссылка на тунель Ngrock + ссылка на путь контроллера

            await botClient.SetWebhookAsync(hook,
                allowedUpdates: new[] { UpdateType.Message,UpdateType.CallbackQuery});

            //Настройка команд для API
            await botClient.SetMyCommandsAsync(botCommandList); 

            return botClient;

        }

        public static InlineKeyboardMarkup GetFuncKeyboard()
        {
            List<InlineKeyboardButton[]> keyboard = new List<InlineKeyboardButton[]>();
            
            foreach (var callback in callbackList)
            {
                keyboard.Add(new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData(callback.ButtonName, callback.Name)});
            }
            return new InlineKeyboardMarkup(keyboard);
        }


    }
} 
