
using Microsoft.EntityFrameworkCore;
using CronWebApi.Models;
using Cron.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.OpenApi.Models;

namespace CronWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RequestHistoryDbContext>(opt =>
                opt.UseInMemoryDatabase("RequestHistory"));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "IP address details Web API",
                    Description = "Тестовое задание от CRON-IT\n"
                });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.MapGet("/{requestedIP}", async (string requestedIP, RequestHistoryDbContext db) =>
            {
                if (!db.RequestHistoryItems.Any(x => x.RequestData == requestedIP))
                {
                    db.RequestHistoryItems.Add(new RequestHistoryItem { RequestData = requestedIP });
                    db.SaveChanges();
                }
                               
                var responce = IpAddressDetails.Get(requestedIP).Result;
                var ipAddressDetails = await responce.Content.ReadAsStringAsync();

                return Results.Content(ipAddressDetails, "text/plain", Encoding.UTF8, (int)responce.StatusCode);
            });

            app.Run();
        }
    }
}
