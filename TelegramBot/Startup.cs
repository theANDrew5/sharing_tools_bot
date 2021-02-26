using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using tel_bot_net.Models;
using tel_bot_net.Services;

namespace tel_bot_net
{
    public class Startup
    {

        public Startup(IConfiguration config)
        {
            Configuration = config;// ������������ �� ���������
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)// ����������� ��� ��������
        {

            services.
                AddControllers().
                AddNewtonsoftJson()
                ; // ��������� ����������� MVC

            services.AddTransient<MessageHandlerService>();//������ ��������� ���������
            services.AddTransient<CallbackHandlerService>();//������ ��������� ������� ������
            services.AddTransient<DataBaseService>();//������ ���� ������
            //services.AddSingleton<ReplyHandlerService>();//������ ��������� � �������� ���������

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // ��������� ��������� ��������
        {
            // ���� ���������� � �������� ����������
            if (env.IsDevelopment())
            {
                // �� ������� ���������� �� ������, ��� ������� ������
                app.UseDeveloperExceptionPage();
            }

            //��������� ����������� �������������
            app.UseRouting();

            // ������������� ������, ������� ����� ��������������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();//������������� �� ������ ���������
            });

            Bot.GetBotClientAsync().Wait();// ��������� ����

        }
    }
}
