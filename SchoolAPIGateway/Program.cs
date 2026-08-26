using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Configuration
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

// 🛠 Services
builder.Services.AddControllers();
builder.Services.AddOcelot();

var app = builder.Build();

// 🌐 Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Ocelot middleware
await app.UseOcelot();

app.Run();
