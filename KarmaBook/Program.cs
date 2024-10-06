using KarmaBook.Context;
using KarmaBook.Extensions;
using KarmaBook.Models;
using KarmaBook.Repositories;
using KarmaBook.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace KarmaBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // JWT Authentication Config
            JWTSettings settings = new JWTSettings();
            builder.Configuration.GetSection("Jwt").Bind(settings);

            // JWT Bearer Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settings.Issuer,
                        ValidAudience = settings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
                    };
                });

            // Add Authorization Service
            builder.Services.AddAuthorization();

            // Register application services
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();
            builder.Services.AddAutoMapper(typeof(ObjectDtoMapper)); // AutoMapper for DTO mappings
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));

            // Configure DbContext with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            });

            // Register TokenService for JWT generation
            builder.Services.AddScoped<TokenService>();

            // Add controllers to the service container
            builder.Services.AddControllers();

            // Configure Swagger with JWT Bearer authentication
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                    Enter 'Bearer' [space] and then your token in the text input below.
                                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            // Build the app
            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable HTTPS redirection
            app.UseHttpsRedirection();

            // Enable Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controllers to endpoints
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
