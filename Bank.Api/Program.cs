using BankLibrary.Data;
using BankLibrary.Interfaces;
using BankLibrary.Services;
using BankLibrary.Models;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Load configuration from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container
builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(connectionString));

// Register UserService and other services
builder.Services.AddScoped<IUserService, UserService>();

// Configure JSON serialization to handle circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

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

// Initialize the bank if it does not exist
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BankContext>();

    if (!context.Banks.Any())
    {
        var bank = new BankLibrary.Models.Bank { BankName = "Global Bank" };
        context.Banks.Add(bank);
        context.SaveChanges();
        Console.WriteLine("Bank initialized successfully.");
    }
}


app.Run();
