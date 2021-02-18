using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using tel_bot_net.Services;
using Telegram.Bot.Types;


namespace tel_bot_net
{

    public class Program
    {
        public static UpdateHolderService UpdateHolder = new UpdateHolderService();
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); //запуск
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); //инициализация веб сервера, сервер описывается в классе Startup
                });
    }
}
