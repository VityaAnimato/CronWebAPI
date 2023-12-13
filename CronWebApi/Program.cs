
using Microsoft.EntityFrameworkCore;
using CronWebApi.Models;
using Cron.Core.Services;

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
            builder.Services.AddSwaggerGen();

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

                return IpAddressDetails.Get(requestedIP).Result;
            });

            app.Run();
        }
    }
}
