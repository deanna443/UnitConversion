using UnitConverstion.CommandHandlers;
using UnitConverstion.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register conversion service
builder.Services.AddSingleton<IUnitConverter, UnitConverter>();

// Register CQRS command handler
builder.Services.AddTransient<ConvertUnitCommandHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();