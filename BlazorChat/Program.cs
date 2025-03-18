using ApplicationCore.Commons.Repository;
using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;
using BlazorChat;
using BlazorChat.Components;
using Infrastructure.Memory;
using Infrastructure.Memory.Generators;
using Infrastructure.Memory.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGenericRepository<ChatUser, int>, MemoryGenericRepository<ChatUser, int>>();

builder.Services.AddScoped<IChatUserService, ChatUserService>();

builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapHub<BlazorChatSampleHub>(BlazorChatSampleHub.HubUrl);

app.Run();
