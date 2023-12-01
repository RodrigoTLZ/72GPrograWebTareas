using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using U3Tarea1___Hamburguesas.Models.Entities;
using U3Tarea1___Hamburguesas.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<Repository<Clasificacion>>();
builder.Services.AddTransient<Repository<Menu>>();
builder.Services.AddTransient<MenuRepository>();
builder.Services.AddDbContext<NeatContext>(x=> x.UseMySql("server=localhost;user=root;password=root;database=neat", 
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")));



builder.Services.AddMvc();
var app = builder.Build();

app.UseFileServer();
app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapDefaultControllerRoute();

app.Run();
