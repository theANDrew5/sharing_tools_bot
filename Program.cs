using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace tel_bot_net
{

    public class Program
    {
        //���� �������� ������ ������������
        public static List<long> ReplyChatIds = new List<long>();
        //������� ���������� � ��������
        public static Queue<Message> ReplyMessages = new Queue<Message>();

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); //������
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); //������������� ��� �������, ������ ����������� � ������ Startup
                });
    }
}
