using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using tel_bot_net.Models;



namespace tel_bot_net
{

    public class Program
    {

        //public static AppSettings AppSettings;
        
        public static void Main(string[] args)
        {
            string executePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //создаём необходимые директории
            string filePath = $"{executePath}\\src";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath = $"{executePath}\\src\\transactions";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath = $"{executePath}\\src\\transactions\\open";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath = $"{executePath}\\src\\transactions\\close";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            //читаем botToken
            filePath = $"{executePath}\\src\\BotToken.txt";

            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                using (FileStream fstream = new FileStream(filePath, FileMode.Open))
                {
                    if (fstream.Length != 0)
                    {
                        byte[] array = new byte[fstream.Length];
                        fstream.Read(array, 0, array.Length);
                        string botToken = System.Text.Encoding.Default.GetString(array);
                        AppSettings.Key = botToken;
                        Console.WriteLine($"Your bot token: {AppSettings.Key}");
                    }
                    else
                    {
                        Console.WriteLine($"Write bot token to:\n{filePath}\n" + "And try again.");
                        return;
                    }
                }
            }
            else
            {
                fileInfo.Create();
                Console.WriteLine($"Write bot token to:\n{filePath}\n" + "And try again.");
                return;
            }

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
