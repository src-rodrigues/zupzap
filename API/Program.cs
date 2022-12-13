

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:7047");

builder.Services.AddControllers();
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

string appSettings = "appsettings.json";

if (app.Environment.IsDevelopment())
{
    appSettings = "appsettings.Development.json";
}

var config = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile(appSettings, optional: false)
.Build();

Environment.SetEnvironmentVariable("StringConexao", config.GetValue<string>("StringConexao"));


app.UseStaticFiles();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
