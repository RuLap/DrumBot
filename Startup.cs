using System;
using DrumBot.Commands;
using DrumBot.Entities;
using DrumBot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DrumBot
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<DrumBotDBContext>(opt => opt.UseSqlServer(_configuration.GetConnectionString("DrumBot")), ServiceLifetime.Singleton);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrumBot API", Version = "v1" });
            });
            services.AddSingleton<TelegramBot>();
            services.AddSingleton<BaseCommand, StartCommand>();
            services.AddSingleton<BaseCommand, AddJournalWriteCommand>();
            services.AddSingleton<BaseCommand, DownloadBookCommand>();
            services.AddSingleton<BaseCommand, AddTempoCommand>();
            services.AddSingleton<BaseCommand, AddTimeCommand>();
            services.AddSingleton<BaseCommand, FinishInputCommand>();
            services.AddSingleton<ICommandExecutor, CommandExecutor>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IJournalService, JournalService>();
            services.AddSingleton<IDrumTaskService, DrumTaskService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DrumBot API"));
            serviceProvider.GetRequiredService<TelegramBot>().GetBot().Wait();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}