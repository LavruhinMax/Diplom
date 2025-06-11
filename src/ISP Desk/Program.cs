using ISP_Desk.Data;
using ISP_Desk.ViewModel;
using ISP_Desk.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlite(connectionString));

builder.Services.AddScoped<Start_VM>();
builder.Services.AddScoped<Dispatcher_VM>();
builder.Services.AddScoped<Installator_VM>();
builder.Services.AddScoped<Lead_VM>();
builder.Services.AddScoped<Unit_VM>();
builder.Services.AddScoped<CR_VM>();
builder.Services.AddScoped<TR_VM>();
builder.Services.AddScoped<CryptoService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
