using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi necessari
builder.Services.AddEndpointsApiExplorer(); // Aggiungi supporto per l'API Explorer
builder.Services.AddSwaggerGen(c =>  // Aggiungi configurazione Swagger/OpenAPI
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configura la pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Attiva Swagger
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API v1"));
}

app.UseHttpsRedirection();



app.MapGet("/user", () =>
{
    var names = new[]
    {
        "Tony", "Billy", "Claude", "Steph", "Mike", "Will", "Barney", "Hugo", "Freddy", "James",
        "Alice", "Sophia", "Emma", "Olivia", "Liam", "Noah", "Lucas", "Mia", "Chloe", "Isabella"
    };

    var works = new[] { "Engineer", "Doctor", "Artist", "Teacher", "Musician", "Writer", "Chef", "Architect", "Pilot", "Scientist" };

    var forecast = Enumerable.Range(1, 5).Select(index =>
    {
        var birthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-Random.Shared.Next(18, 60)).AddDays(-Random.Shared.Next(1, 365)));
        var age = DateTime.Now.Year - birthDate.Year;
        var daysSinceBirth = (DateTime.Now - birthDate.ToDateTime(TimeOnly.MinValue)).Days;
        return new PersonInfo(
            names[Random.Shared.Next(names.Length)],
            birthDate,
            age,
            works[Random.Shared.Next(works.Length)],
            daysSinceBirth
        );
    })
    .ToArray();
    return forecast;
})
.WithName("GetUser");

app.Run();

record PersonInfo(string Name, DateOnly BirthDate, int Age, string Sex, int DaysSinceBirth);
 