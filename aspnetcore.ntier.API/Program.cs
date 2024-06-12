
using aspnetcore.ntier.API;
using aspnetcore.ntier.BLL;
using aspnetcore.ntier.BLL.Services;
using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL;
using aspnetcore.ntier.DAL.DataContext;
using aspnetcore.ntier.DAL.Repositories;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
                          policy =>
                          {
                              policy.WithOrigins("https://localhost:4200").AllowAnyMethod();
                          });
});

builder.Services.AddControllers();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ISubtaskService, SubtaskService>();
builder.Services.AddScoped<ISubtaskRepository, SubtaskRepository>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<IAlpacaService, AlpacaService>();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IConnectionService, ConnectionService>();
builder.Services.AddSignalR();


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AspNetCoreNTierDbContext>().AddDefaultTokenProviders();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.RegisterDALDependencies(builder.Configuration);
builder.Services.RegisterBLLDependencies(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseCors(builder =>
        builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", description.GroupName.ToString());
    }
});

app.MapHub<SignalRHub>("signal-hub");

app.Run();

public partial class Program { }