
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VacancyOneTestTask.DataAccess;

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

            // builder.Services.AddSqlite<VacancyOneDbContext>("Filename=:memory:");

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

            await using (var scope = app.Services.CreateAsyncScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<VacancyOneDbContext>().Database;
                // await db.EnsureCreatedAsync();
                await db.MigrateAsync();
            }

            app.Run();
        }
    }
}
