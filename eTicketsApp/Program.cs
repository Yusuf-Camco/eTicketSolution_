using eTickets.Data;
using eTicketsApp.Data;
using eTicketsApp.Data.Base;
using eTicketsApp.Data.Services.Implementations;
using eTicketsApp.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Connstr")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IActorService,ActorService>();
builder.Services.AddScoped<IProducerService, ProducerServices>();
builder.Services.AddScoped<ICinemaService,CinemaService>();
builder.Services.AddScoped<IMovieService,MovieService>();


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

//Seed DB
AppDbInitializer.Seed(app);

app.Run();
