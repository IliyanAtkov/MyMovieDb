using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyMovieDb.Data;
using MyMovieDb.API.Extensions;
using MyMovieDb.Services.Constants;

namespace MyMovieDb.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.ValidateConfiguration();

            services.AddAuthenticationWithJwt(Configuration);
            services.AddDbContext<MyMovieDbContext>(options =>options.UseSqlServer(Configuration[ConfigurationNamesConstants.ConnectionString]));
            services.AddIdentity();

            services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
            services.ConfigureMovieDbApiHttpClient(Configuration);

            services.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
