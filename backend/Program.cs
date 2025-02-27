using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using backend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Default") 
                         ?? builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenPolicy", config =>
    {
        config.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

tusing (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    InitializeData(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("OpenPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void InitializeData(AppDbContext context)
{
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product { Name = "Item A", Description = "Description A", Price = 15.5m },
            new Product { Name = "Item B", Description = "Description B", Price = 25.0m },
            new Product { Name = "Item C", Description = "Description C", Price = 35.75m }
        );
        context.SaveChanges();
    }
}