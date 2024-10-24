using System.Configuration;
using System.Globalization;
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InTouch.Application;
using InTouch.Infrastructure;
using InTouch.Infrastructure.Data;
using InTouch.UserService.Core;
using InTouch.UserService.Extensions;
using InTouch.UserService.Query;
using MediatR;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Add services to the container.
builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers();

builder.Services.AddRegisterTypeHandler();
builder.Services.AddInfrastructure();
builder.Services.AddCommandHandlers();
builder.Services.AddQueryHandlers();
builder.Services.AddWriteOnlyRepositories();
builder.Services.AddResponseMediatr();
builder.Services.AddReadDbContext();
builder.Services.AddReadOnlyRepositories();


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
app.UseErrorHandling();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();