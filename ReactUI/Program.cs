using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.AutoFac;
using Core.Configuration;
using Core.Extensions;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("countries.json", true, true)
    .AddEnvironmentVariables();

// Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(p => p.RegisterModule(new AutoFacBussinesModule()));

builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOption"));
builder.Services.Configure<List<AddressConfiguration>>(builder.Configuration.GetSection("Countries"));

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"), mig => mig.MigrationsAssembly("Data"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomTokenAuth(builder.Configuration.GetSection("TokenOption").Get<CustomTokenOptions>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
