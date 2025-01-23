using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add context to the dbo
builder.Services.AddDbContext<WebAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAPIConnection"), b =>
        b.MigrationsAssembly("WebAPI.Identity")));

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<WebAPIDbContext>()
.AddDefaultTokenProviders();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
