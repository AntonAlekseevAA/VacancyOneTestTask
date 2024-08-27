
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VacancyOneTestTask.Abstractions;
using VacancyOneTestTask.BL;
using VacancyOneTestTask.DataAccess;
using VacancyOneTestTask.DataAccess.Repositories;

namespace VacancyOneTestTask
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            using var dbConn = new SqliteConnection("Filename=:memory:");
            await dbConn.OpenAsync();
            builder.Services.AddDbContext<VacancyOneDbContext>(dbOptions => dbOptions.UseSqlite(dbConn));

            RegisterServices(builder.Services);

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

            await Seed(app.Services);

            app.Run();
        }

        private static IServiceCollection RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<ITasksService, TasksService>();
            return services;
        }

        private static async Task Seed(IServiceProvider services)
        {
            await using (var scope = services.CreateAsyncScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VacancyOneDbContext>();
                var db = context.Database;
                
                await db.MigrateAsync();


                var task1 = new DataAccess.Entities.TicketEntity
                {
                    Date = new DateTime(2024 - 01 - 01),
                    Status = DataAccess.Entities.TicketStatus.Created,
                    Files = new List<DataAccess.Entities.AttachedFile>
                    {
                        new() { Url = "https://www.example.com/file1" },
                        new() { Url = "https://www.example.com/file2"  }
                    }
                };
                context.Tasks.Add(task1);

                var task2 = new DataAccess.Entities.TicketEntity
                {
                    Date = new DateTime(2023 - 01 - 01),
                    Status = DataAccess.Entities.TicketStatus.OnReview,
                    Files = new List<DataAccess.Entities.AttachedFile>
                    {
                        new() { Url = "https://www.example.com/file3" },
                        new() { Url = "https://www.example.com/file4"  }
                    }
                };
                context.Tasks.Add(task2);

                await context.SaveChangesAsync();
            }
        }
    }
}
