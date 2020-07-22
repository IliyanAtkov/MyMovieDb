using System;
using Microsoft.Extensions.Configuration;
using MyMovieDb.API.Constants;

namespace MyMovieDb.API.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ValidateConfiguration(this IConfiguration configuration)
        {
            foreach (var field in typeof(ConfigurationNamesConstants).GetFields())
            {
                var fieldValue = field.GetValue(null)?.ToString();
                if (configuration[fieldValue] == null)
                {
                    throw new ArgumentNullException(fieldValue);
                }
            }
        }
    }
}