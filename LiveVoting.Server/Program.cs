using FluentValidation;
using LiveVoting.Server.Data;
using LiveVoting.Server.Repositories.User;
using LiveVoting.Server.Services.Email;
using LiveVoting.Server.Services.Hashing;
using LiveVoting.Server.Services.User;
using LiveVoting.Server.Validators;
using LiveVoting.Shared.Models;
using Microsoft.EntityFrameworkCore;
using IEmailSender = Microsoft.AspNetCore.Identity.UI.Services.IEmailSender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        builder =>
            builder.WithOrigins("http://localhost:5235")
                .AllowAnyMethod()
                .AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("LocalDb");

Console.WriteLine("Connection string: " + connectionString);

builder.Services.AddDbContext<VotingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IValidator<SignupModel>, SignupValidator>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConfirmationEmailSender, ConfirmationEmailSender>();



//TODO Register services
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

