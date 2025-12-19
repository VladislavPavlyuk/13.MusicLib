using Microsoft.EntityFrameworkCore;
using MusicLib.BLL.Interfaces;
using MusicLib.BLL.Services;
using MusicLib.BLL.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using MusicLib.DAL.EF;


var builder = WebApplication.CreateBuilder(args);

// Configure localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Set supported cultures
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "uk-UA" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

// Configure session storage
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Get database connection string from configuration
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddMusicLibContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAudioService, AudioService>();
builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<ISongService, SongService>();

// Add MVC services
builder.Services.AddControllersWithViews();

// Configure CORS for React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MusicLibContext>();
    DbInitializer.Initialize(context);
}

app.UseRouting();
app.UseCors("AllowReactApp");

// Disable HTTPS redirect in development
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseSession();
app.UseStaticFiles();

// Configure localization
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
