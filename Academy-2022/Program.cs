using Academy_2022.Data;
using Academy_2022.Options;
using Academy_2022.Repositories;
using Academy_2022.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db configuration
builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
 optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("ApplicationDbContext")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

// sample injection
builder.Services.AddScoped<IDiTestAService, DiTestAService>();
// sample injection
builder.Services.AddTransient<IDiTestBService, DiTestBService>();

// repository injection
builder.Services.AddScoped<IUserRepository, UserRepository>();

// service injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// add auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// sample config read
var applicationDbContextPath = builder.Configuration.GetSection("ConnectionStrings:ApplicationDbContext");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
