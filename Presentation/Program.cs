using Microsoft.EntityFrameworkCore;
using Presentation.Contexts;
using Presentation.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
);

builder.Services.AddScoped<ArtistService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
