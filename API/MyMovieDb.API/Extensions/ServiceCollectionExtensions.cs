using Microsoft.Extensions.DependencyInjection;
using MyMovieDb.Services.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace MyMovieDb.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static string MyMovieDbName = "MyMovieDb";
        private static string MyMovieDbServicesName = "MyMovieDb.Services";
        private static bool loadedAssemblies = false;

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            LoadAssemblies();

            AppDomain.CurrentDomain.GetAssemblies()
               .Where(t => t.FullName.Contains(MyMovieDbServicesName))
               .SelectMany(s => s.GetTypes().Where(t => t.IsClass && t.GetInterfaces().Any(v => v.Name == nameof(IService)) && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}")))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
              .ToList()
              .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
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
