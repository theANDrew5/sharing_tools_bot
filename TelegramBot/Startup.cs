using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Models;
using TelegramBot.Services;

namespace TelegramBot
{
    public class Startup
    {

        public Startup(IConfiguration config)
        {
            Configuration = config;// конфигураци€ по умолчанию
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)// подключение веб сервисов
        {

            services.
                AddControllers().
                AddNewtonsoftJson()
                ; // добавл€ем контроллеры MVC

            services.AddTransient<MessageHandlerService>();//—ервис перехвата сообщений
            services.AddTransient<CallbackHandlerService>();//—ервис перехвата нажатий кнопок
            //services.AddTransient<DBMethods>();//—ервис базы данных
            //services.AddSingleton<ReplyHandlerService>();//—ервис перехвата и хранени€ сообщений

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // настройка обработки запросов
        {
            // если приложение в процессе разработки
            if (env.IsDevelopment())
            {
                // то выводим информацию об ошибке, при наличии ошибки
                app.UseDeveloperExceptionPage();
            }

            //добавл€ем возможности маршрутизации
            app.UseRouting();

            // устанавливаем адреса, которые будут обрабатыватьс€
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();//маршрутизаци€ на основе атрибутов
            });

            Bot.GetBotClientAsync().Wait();// запускаем бота

        }
    }
}
