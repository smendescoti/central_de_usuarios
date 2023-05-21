using CentralDeUsuarios.Application.Interfaces;
using CentralDeUsuarios.Application.Services;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Interfaces.Security;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Domain.Services;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Repositories;
using CentralDeUsuarios.Infra.Logs.Contexts;
using CentralDeUsuarios.Infra.Logs.Interfaces;
using CentralDeUsuarios.Infra.Logs.Persistence;
using CentralDeUsuarios.Infra.Logs.Settings;
using CentralDeUsuarios.Infra.Messages.Helpers;
using CentralDeUsuarios.Infra.Messages.Producers;
using CentralDeUsuarios.Infra.Messages.Settings;
using CentralDeUsuarios.Infra.Security.Services;
using CentralDeUsuarios.Infra.Security.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CentralDeUsuarios.Services.Api
{
    /// <summary>
    /// Classe para configuração o contexto de injeção de dependência do AspNet
    /// </summary>
    public static class Setup
    {
        public static void AddRegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUsuarioAppService, UsuarioAppService>();
            builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddEntityFrameworkServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("CentralDeUsuarios");
            builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddMessageServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MessageSettings>(builder.Configuration.GetSection("MessageSettings"));
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddTransient<MessageQueueProducer>();
            builder.Services.AddTransient<MailHelper>();
        }

        public static void AddAutoMapperServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddMediatRServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddMongoDBServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

            builder.Services.AddSingleton<MongoDBContext>();
            builder.Services.AddTransient<ILogUsuariosPersistence, LogUsuariosPersistence>();
        }

        public static void AddJwtBearerSecurity(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddTransient<IAuthorizationSecurity, AuthorizationSecurity>();

            builder.Services.AddAuthentication(
                auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(
                bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(                            Encoding.ASCII.GetBytes                                (builder.Configuration.GetSection("JwtSettings").GetSection("SecretKey").Value)                            ),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API - Central de Usuários",
                    Description = "API REST para controle de usuários. Treinamento C# Avançado Formação Arquiteto - COTI Informática",
                    Contact = new OpenApiContact { Name = "COTI Informática", Email = "contato@cotiinformatica.com.br", Url = new Uri("http://www.cotiinformatica.com.br") }
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void AddCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                   s => s.AddPolicy("DefaultPolicy", builder =>
                   {
                       builder.AllowAnyOrigin() //qualquer origem pode acessar a API
                              .AllowAnyMethod() //qualquer método (POST, PUT, DELETE, GET)
                              .AllowAnyHeader(); //qualquer informação de cabeçalho
                   })
               );
        }

        public static void UseCors(this WebApplication app)
        {
            app.UseCors("DefaultPolicy");
        }
    }
}
