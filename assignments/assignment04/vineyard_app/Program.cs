using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace VineyardApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class Vineyard
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<string> Grapes { get; set; }

        public void PrintDetails()
        {
            Console.WriteLine($"Vineyard: {Name}, Location: {Location}, Grapes: {string.Join(", ", Grapes)}");
        }
    }

    // Sección de código espagueti
    public class VineyardManager
    {
        private List<Vineyard> vineyards = new List<Vineyard>();

        public void AddVineyard(string name, string location, List<string> grapes)
        {
            var vineyard = new Vineyard { Name = name, Location = location, Grapes = grapes };
            vineyards.Add(vineyard);
            vineyard.PrintDetails(); // Método que puede mejorarse
        }
    }
}
