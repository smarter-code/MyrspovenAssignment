using Microsoft.EntityFrameworkCore;
using MyrspovenAssignment.Infrastructure;
using MyrspovenAssignment.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//To cache the access token
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<MyrspovenAssignmentContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepository<Building>, BuildingRepository>();
builder.Services.AddScoped<IRepository<Signal>, SignalRepository>();
builder.Services.AddScoped<IRepository<SignalData>, SignalDataRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

using (var scope = scopedFactory.CreateScope())
{
    var soccerManagerContext = scope.ServiceProvider.GetService<MyrspovenAssignmentContext>();
    soccerManagerContext.Database.Migrate();
}

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

app.Run();
