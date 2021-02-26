
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace tel_bot_net.Singletones
{
     public class ReplyHandler
    {
        private static readonly ReplyHandler instance = new ReplyHandler();

        private static List<long> IdsReplies;
        private static Dictionary<long, Update> Replies;

        private ReplyHandler()
        {
            IdsReplies = new List<long>();
            Replies = new Dictionary<long, Update>();
        }

        public static ReplyHandler GetInstance()
        {
            return instance;
        }

        //помещаем id диалога и ссылку на поток в ожидание
        public void WaitReply (long id)
        {
            IdsReplies.Add(id);
        }

        //проверяем ожидаем ли мы этот update перехват если да
        public bool Hold (Update update)
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
        public Update DeHold (long id)
        {
            while (!Replies.ContainsKey(id))
            {
                    Task.Delay(500).Wait();
            }
            Update update = Replies[id];
            Replies.Remove(id);
            return update;
        }

        public async Task<Update> DeHoldAsync(long id)
        {
            return await Task.Run(() => DeHold(id));
        }
    }

}
