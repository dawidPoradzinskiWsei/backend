
using ApplicationCore.Commons.Repository;
using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;
using Infrastructure.Memory;
using Infrastructure.Memory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IGenericRepository<Quiz, int>, MemoryGenericRepository<Quiz, int>>();
builder.Services.AddSingleton<IGenericRepository<QuizItem, int>, MemoryGenericRepository<QuizItem, int>>();
builder.Services.AddSingleton<IGenericRepository<QuizItemUserAnswer, string>, MemoryGenericRepository<QuizItemUserAnswer, string>>();

builder.Services.AddTransient<IQuizAdminService, QuizAdminService>();
builder.Services.AddTransient<IQuizUserService, QuizUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Seed();
app.Run();
