using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;



namespace tel_bot_net
{

    public class Program
    {
        
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
