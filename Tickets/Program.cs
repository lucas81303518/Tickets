using Tickets.Data;
using Microsoft.EntityFrameworkCore;
using Tickets.Models;
using Tickets.Services;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TicketsContext");

builder.Services.AddDbContext<TicketsContext>
    (options =>
    {
        options.UseLazyLoadingProxies()
               .UseNpgsql(connectionString);//.UseInMemoryDatabase(databaseName: "TestDatabase");
    });
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TicketsContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
