using System.Globalization;
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InTouch.Application;
using InTouch.UserService.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers();

builder.Services.AddCommandHandlers();
builder.Services.AddResponseMediatr();

builder.Services.AddSwaggerGen();



// FluentValidation global configuration.
ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member?.Name;
ValidatorOptions.Global.LanguageManager = new LanguageManager { Enabled = true, Culture = new CultureInfo("en-US") };


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();