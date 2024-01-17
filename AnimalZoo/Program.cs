
using AnimalZoo.Database;
using AnimalZoo.Repositories.AnimalsReposiotry;
using AnimalZoo.Repositories.EnclosureRepository;
using AnimalZoo.Services;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace AnimalZoo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ZooDbContext>(opt => opt.UseInMemoryDatabase("Zoo"));
            builder.Services.AddScoped<IAnimals, AnimalsRepository>();
            builder.Services.AddScoped<IEnclosure, EnclosureRepository>();

            builder.Services.AddScoped<ISortingService, SortingService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
