using Entity.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddScoped<ILoaiRepository, LoaiRepository>();*/

/*builder.Services.AddDbContext<JobiverseContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AivenConnection"))
);*/
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
