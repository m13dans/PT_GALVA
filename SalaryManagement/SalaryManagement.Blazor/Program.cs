using CurrieTechnologies.Razor.SweetAlert2;
using SalaryManagement.Application.GajiFeature;
using SalaryManagement.Application.PegawaiFeature;
using SalaryManagement.Blazor.Components;
using SalaryManagement.Infrastructure.Data;
using SalaryManagement.Infrastructure.GajiFeature;
using SalaryManagement.Infrastructure.Migrations;
using SalaryManagement.Infrastructure.PegawaiFeature;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SQLConnection")
    ?? "Data Source=.;Database=PT_Galva_SalaryManagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;";

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<DbConnectionFactory>();

builder.Services.AddScoped<PegawaiService>();
builder.Services.AddScoped<IPegawaiRepository, PegawaiRepository>();

builder.Services.AddScoped<GajiService>();
builder.Services.AddScoped<IGajiRepository, GajiRepository>();

builder.Services.AddSweetAlert2();

var app = builder.Build();

DbInitializer.Initialize(connectionString);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
