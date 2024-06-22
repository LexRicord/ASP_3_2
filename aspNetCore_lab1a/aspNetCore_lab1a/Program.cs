using aspNetCore_lab1a;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddTransient<task1_middleware>(next => new task1_middleware(next.GetRequiredService<RequestDelegate>()));
builder.Services.AddTransient<task2_middleware>(next => new task2_middleware(next.GetRequiredService<RequestDelegate>()));
builder.Services.AddTransient<task3_middleware>(next => new task3_middleware(next.GetRequiredService<RequestDelegate>()));
builder.Services.AddTransient<task4_middleware>(next => new task4_middleware(next.GetRequiredService<RequestDelegate>()));
builder.Services.AddTransient<task5_middleware>(next => new task5_middleware(next.GetRequiredService<RequestDelegate>()));
builder.Services.AddTransient<task6_middleware>(next => new task6_middleware(next.GetRequiredService<RequestDelegate>(), next.GetRequiredService<ITempDataProvider>()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.Map("/task4", app =>
{
    app.UseMiddleware<task4_middleware>();
});

app.Map("/task5", app =>
{
    app.UseMiddleware<task5_middleware>();
});

app.Map("/task6", app =>
{
    app.UseMiddleware<task6_middleware>();
});

app.MapWhen(context =>
{
    var path = context.Request.Path;
    Console.WriteLine($"Request Path 1: {path}");
    return path.StartsWithSegments("/task1") && path.Value.EndsWith(".hae") && context.Request.Method == "GET";
}, appBuilder =>
{
    appBuilder.UseMiddleware<task1_middleware>();
});

app.MapWhen(context =>
{
    var path = context.Request.Path;
    return path.StartsWithSegments("/task2") && path.Value.EndsWith(".hae") && context.Request.Method == "POST";
}, appBuilder =>
{
    appBuilder.UseMiddleware<task2_middleware>();
});

app.MapWhen(context =>
{
    var path = context.Request.Path;
    return path.StartsWithSegments("/task3") && path.Value.EndsWith(".hae") && context.Request.Method == "PUT";
}, appBuilder =>
{
    appBuilder.UseMiddleware<task3_middleware>();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
    