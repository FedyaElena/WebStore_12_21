var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Загрузка информации из файла конфигурации

//var configuration = app.Configuration;
//var greetings = app.Configuration["CustomGreetings"];

app.MapGet("/", () => app.Configuration["CustomGreetings"]);

app.Run();
