using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

// Adaugă serviciile pentru controlere și vizualizări
builder.Services.AddControllersWithViews();

// Configurează DbContext-ul
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection")));

// Adaugă repository-ul pentru produse
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

// Verifică conexiunea la baza de date la pornirea aplicației
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<StoreDbContext>();
        Console.WriteLine("StoreDbContext resolved successfully.");

        // Testează accesul la produsele din baza de date
        var productsTest = dbContext.Products.Any();
        Console.WriteLine($"Can access products: {productsTest}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during database access test: {ex.Message}");
    }
}

// Configurează rutarea
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
