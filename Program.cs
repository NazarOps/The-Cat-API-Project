
using Cat_API_Project.Data;
using Cat_API_Project.DTO.External;
using Cat_API_Project.Services;
using Cat_API_Project.Services.External;
using Cat_API_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar;
using Scalar.AspNetCore;

namespace Cat_API_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddHttpClient("TheCatApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["TheCatApi:BaseUrl"]!);
                client.DefaultRequestHeaders.Add("x-api-key", builder.Configuration["TheCatApi:ApiKey"]!);
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICatService, CatService>();
            builder.Services.AddScoped<ITheCatApiService, TheCatApiService>();

            builder.Services.AddScoped<IBreedImportService, BreedImportService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            

            app.Run();
        }
    }
}
