﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyMovieDb.Data;
using MyMovieDb.Data.Models;
using MyMovieDb.Services.Constants;
using MyMovieDb.Services.Interfaces;
using MyMovieDb.Services.TheMovieDb.Implementations;
using Polly;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace MyMovieDb.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static string MyMovieDbName = "MyMovieDb";
        private static string MyMovieDbServicesName = "MyMovieDb.Services";
        private static bool loadedAssemblies = false;

        public static void AddAuthenticationWithJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration[ConfigurationNamesConstants.JwtIssuer],
                        ValidAudience = configuration[ConfigurationNamesConstants.JwtAuidence],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ConfigurationNamesConstants.JwtKey]))
                    };
                });
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            })
                .AddEntityFrameworkStores<MyMovieDbContext>();
        }

        public static void ConfigureMovieDbApiHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<TheMovieDbHttpService>(c =>
            {
                c.BaseAddress = new Uri(configuration[ConfigurationNamesConstants.TheMovieDbUrl]);
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration[ConfigurationNamesConstants.TheMovieDbToken]);
            })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(
                    int.Parse(configuration[ConfigurationNamesConstants.TheMovieDbPollyRetryCount]),
                    _ => TimeSpan.FromMilliseconds(double.Parse(configuration[ConfigurationNamesConstants.TheMovieDbPollyRetryTimeInMiliseconds]))))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(
                    int.Parse(configuration[ConfigurationNamesConstants.TheMovieDbPollyCircuitBreakerCount]),
                    TimeSpan.FromSeconds(double.Parse(configuration[ConfigurationNamesConstants.TheMovieDbPollyCircuitBreakerTimeInSeconds]))));
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            LoadAssemblies();
            LoadServices(services);
            LoadSingletonServices(services);

            return services;
        }

        private static void LoadServices(IServiceCollection services)
        {
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(t => t.FullName != null && t.FullName.Contains(MyMovieDbServicesName))
                .SelectMany(s => s.GetTypes().Where(t => t.IsClass && t.GetInterfaces().Any(v => v.Name == nameof(IService)) && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}")))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));
        }

        private static void LoadSingletonServices(IServiceCollection services)
        {
            var test = AppDomain.CurrentDomain.GetAssemblies()
               .Where(t => t.FullName != null && t.FullName.Contains(MyMovieDbServicesName))
               .SelectMany(s => s.GetTypes().Where(t => t.IsClass && t.GetInterfaces().Any(v => v.Name == nameof(ISingletonService)) && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}")))
               .Select(t => new
               {
                   Interface = t.GetInterface($"I{t.Name}"),
                   Implementation = t
               })
               .ToList();

            AppDomain.CurrentDomain.GetAssemblies()
               .Where(t => t.FullName != null && t.FullName.Contains(MyMovieDbServicesName))
               .SelectMany(s => s.GetTypes().Where(t => t.IsClass && t.GetInterfaces().Any(v => v.Name == nameof(ISingletonService)) && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}")))
               .Select(t => new
               {
                   Interface = t.GetInterface($"I{t.Name}"),
                   Implementation = t
               })
               .ToList()
               .ForEach(s => services.AddSingleton(s.Interface, s.Implementation));
        }

        private static void LoadAssemblies()
        {
            if (loadedAssemblies)
            {
                return;
            }

            var refAssembyNames = Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(t => t.FullName.Contains(MyMovieDbName));

            foreach (var asslembyNames in refAssembyNames)
            {
                Assembly.Load(asslembyNames);
            }

            loadedAssemblies = true;
        }
    }
}
