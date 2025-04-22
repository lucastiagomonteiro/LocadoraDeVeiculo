using LocadoraDeVeiculo.Context;
using LocadoraDeVeiculo.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new
    MySqlServerVersion(new Version(8,0,3)))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true; 
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


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

await CriarPerfilsUsuarioAsync(app);

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task CriarPerfilsUsuarioAsync(WebApplication app)
{
    var scopedFatory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFatory.CreateScope())
    {
        var seedService = scope.ServiceProvider.GetRequiredService<ISeedUserRoleInitial>();
        await seedService.SeedRoleAsync();
        await seedService.SeedUserRoleAsync(); ;

    }
}
