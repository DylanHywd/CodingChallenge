using System.Collections.Generic;
using TimesheetSystem.Models;
using Microsoft.AspNetCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Register the In-Memory Database in the services container
builder.Services.AddDbContext<TimesheetDbContext>(options =>
    options.UseInMemoryDatabase("TimesheetDB"));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Timesheet}/{action=Index}/{id?}");

app.Run();