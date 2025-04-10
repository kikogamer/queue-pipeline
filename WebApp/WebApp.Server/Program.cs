using App.Adapters.Amqp.Configuration;
using App.Core.Data;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddWebApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddRabbitMQConfiguration(cfg => cfg.WithConfiguration(builder.Configuration));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);
builder.Services.ResolveDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
