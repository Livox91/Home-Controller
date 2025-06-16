using HomeController.Components;
using HomeController.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddScoped<DbHelper>();
builder.Services.AddScoped<RoomService>();

builder.Services.AddSingleton<DeviceService>();
builder.Services.AddHttpClient<DeviceService>();

var ipAssigner = new IPAssigner(new HttpClient(), builder.Configuration);
await ipAssigner.InitializeAsync();
builder.Services.AddSingleton<IPAssigner>(ipAssigner);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
