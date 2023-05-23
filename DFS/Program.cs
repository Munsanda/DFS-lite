using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DFS.Data;
var builder = WebApplication.CreateBuilder(args);

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

// Add services to the container.
//var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User Id=sa;Password={dbPassword};Integrated Security=False;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False";
//var connectionString = "Data Source=192.168.8.100,8001;Initial Catalog=medicalsupplydb;User Id=sa;Password=password12345#;Encrypt=False";//win11
var connectionString = "Data Source=localhost,7001;Initial Catalog=db;User Id=sa;Password=password12345#;Integrated Security=False;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False";
//string connectionString = "Data Source= docker.host.internal;Initial Catalog=medicalsupplydb;User Id=sa;Password=password12345#Integrated Security=False;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False";

builder.Services.AddDbContext<DFSContext>(x => x.UseSqlServer(connectionString,
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

//builder.Services.AddDbContext<DFSContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DFSContext") ?? throw new InvalidOperationException("Connection string 'DFSContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
