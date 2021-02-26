
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace tel_bot_net.Services
{
    static class ReplyHandler
    {
        private static List<long> IdsReplies = new List<long>();
        private static Dictionary<long,Update> Replies = new Dictionary<long, Update>();

        //помещаем id диалога и ссылку на поток в ожидание
        public static void WaitReply (long id)
        {
            IdsReplies.Add(id);
        }

        //проверяем ожидаем ли мы этот update перехват если да
        public static bool Hold (Update update)
        {
            long id = update.Message.Chat.Id;

            if (IdsReplies.Contains(id))
            {
                Replies.Add(id,update); //помещаем update в контейнер
                IdsReplies.Remove(id); //удаляем чат из ожидания
                return true;
            }

            return false;
        }


        //выдаём update
        public static Update DeHold (long id)
        {
            while (!Replies.ContainsKey(id))
            {
                    Task.Delay(500).Wait();
            }
            Update update = Replies[id];
            Replies.Remove(id);
            return update;
        }

        public static async Task<Update> DeHoldAsync(long id)
        {
            return await Task.Run(() => DeHold(id));
        }
    }

}
