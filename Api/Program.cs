using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.Core.Application.utils;
using Microsoft.AspNetCore.RateLimiting;
using auth.Models;
using auth.Services;
using Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Collections.Generic;

namespace Api

{
    class Program
    {
        
        
        public static async Task Main()
        {
            
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            builder.Services.AddRateLimiter(option =>
            {
                option.AddFixedWindowLimiter("fixed", opt =>
                {
                    opt.PermitLimit = 5; // máximo 100 requisições
                    opt.Window = TimeSpan.FromMinutes(1);
                });
                
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        builder.Configuration.Bind("jwt", options);
                        //builder.Configuration.Bind("jwt", options);

                        // AGORA O PONTO CRÍTICO: Configurar a chave de assinatura
                        var key = builder.Configuration["jwt:Key"]; // Ou o nome exato no seu appsettings.json

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                            ValidateIssuer = true, // Mude para false se não estiver validando o dono do token
                            ValidateAudience = true, // Mude para false se não estiver validando o destino
                            ValidIssuer = builder.Configuration["jwt:Issuer"],
                            ValidAudience = builder.Configuration["jwt:Audience"]
                        };
                    });
                
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",policy=>policy.RequireRole("Admin"));
                options.AddPolicy("User",policy=>policy.RequireRole("User"));
            });
            
            //builder.Services.
            builder.Services.AddTransient<TokenService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira apenas o token JWT no campo abaixo."
                });

                options.AddSecurityRequirement(_ => new OpenApiSecurityRequirement
                {
                    { 
                        new OpenApiSecuritySchemeReference("Bearer"), 
                        new List<string>() 
                    }
                });
            });
            AddScope scope = new();
            Load.LoadEnv();
            
            scope.AddScopeFuncion(builder);
           
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseAuthorization();
            
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.MapGet("/5", () => {return;}).RequireAuthorization();
            app.UseRateLimiter();
            await new Routers.Routers().Teste(app);
            //app.UseHttpsRedirection();
           
            app.Run();
        }
    }
}

