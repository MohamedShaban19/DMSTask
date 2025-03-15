using DMSTaskApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();  
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
//----------------------------------
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("SQLCONNECTION")));
//---------------------------------------------------------------
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
