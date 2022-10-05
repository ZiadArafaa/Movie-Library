using CRUD_Operations.Data;
using CRUD_Operations.Services;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            //connectionString
            builder.Services.AddDbContext<ApplicationDbContext>(c=>c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            //Transient Services
            builder.Services.AddTransient<IGenreServices, GenreServices>();
            builder.Services.AddTransient<IMovieServices, MovieServices>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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