using System.Diagnostics.Eventing.Reader;
using System.Text;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using LiveVoting.Server.Data;
using LiveVoting.Server.Repositories.Candidate;
using LiveVoting.Server.Repositories.Election;
using LiveVoting.Server.Repositories.ElectionRound;
using LiveVoting.Server.Repositories.UpcomingCandidate;
using LiveVoting.Server.Repositories.UpcomingVote;
using LiveVoting.Server.Repositories.User;
using LiveVoting.Server.Services.Candidate;
using LiveVoting.Server.Services.Election;
using LiveVoting.Server.Services.Email;
using LiveVoting.Server.Services.Hashing;
using LiveVoting.Server.Services.Jwt;
using LiveVoting.Server.Services.UpcomingCandidate;
using LiveVoting.Server.Services.User;
using LiveVoting.Server.Services.Voting;
using LiveVoting.Server.Validators;
using LiveVoting.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var clientAddress = builder.Configuration["Client"] ?? throw new ApplicationException("Client is missing from configuration");
Console.WriteLine("Client address in server is: " + clientAddress);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEverybody",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo() { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>(new AWSOptions
{
    Credentials = new BasicAWSCredentials(builder.Configuration["s3Id"], builder.Configuration["s3key"]),
    Region = RegionEndpoint.EUNorth1
});


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "LiveVoting.Server", // Replace with your issuer
            ValidAudience = "LiveVoting.Server", // Replace with your audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecret"])), // Replace with your secret
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("VerifiedUsersOnly", policy =>
        policy.RequireClaim("IsEmailConfirmed", "True"));

    options.AddPolicy("AuthenticatedUsers", policy =>
        policy.RequireAuthenticatedUser());
});


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<IJwtService, JwtService>();





var connectionString = builder.Configuration.GetConnectionString("LocalDb");

Console.WriteLine("Connection string: " + connectionString);

builder.Services.AddDbContext<VotingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

//TODO Add Validators
builder.Services.AddScoped<IValidator<SignupModel>, SignupValidator>();


//TODO Add Repos
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoggedUserRepository, LoggedUserRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IElectionRepository, ElectionRepository>();
builder.Services.AddScoped<IElectionRoundRepository, ElectionRoundRepository>();
builder.Services.AddScoped<IUpcomingCandidateRepository, UpcomingCandidateRepository>();
builder.Services.AddScoped<IUpcomingVoteRepository, UpcomingVoteRepository>();


//TODO Add Others
builder.Services.AddScoped<IConfirmationEmailSender, ConfirmationEmailSender>();
builder.Services.AddScoped<ICandidateService, CandidateService>();


//TODO Register services
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IElectionService, ElectionService>();
builder.Services.AddScoped<IUpcomingCandidateService, UpcomingCandidateService>();
builder.Services.AddScoped<IVotingService, VotingService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowEverybody");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

