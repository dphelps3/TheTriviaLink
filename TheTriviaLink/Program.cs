using DataAccess;
using TriviaLink.Services;
using TriviaApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Data access
builder.Services.AddTransient<IBaseDao, BaseDao>();
builder.Services.AddTransient<IGamesDao, GamesDao>();
builder.Services.AddTransient<IUsersDao, UsersDao>();

// Code generator service
builder.Services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();

// User service
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/Game" || context.Request.Path == "/Game/")
    {
        context.Response.Redirect("/Game/All");
    }
    else
    {
        await next();
    }
});

app.Run();
