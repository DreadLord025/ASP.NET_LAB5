using Welcome;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseErrorLoginMiddleware();
app.MapControllers();

string pathWelcome = "Welcome.json";

WelcomePage welcome = WelcomePage.FromJson(pathWelcome);

app.MapControllers();
app.MapGet("/", () => welcome.Info());
app.Run();
