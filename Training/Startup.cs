using Infrastructure.Data;
using Infrastructure.Data.Initializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Training.FeatureToggle;
using Training.UserService;

namespace Training
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
            services.AddTransient<IDbInitializer, UpdateByMigrationsDbInitializer>();

            services.AddControllersWithViews();

            AddEntityFramework(services);

            services.AddTransient<IUserService, UserService.UserService>();
        }

        public virtual void AddEntityFramework(IServiceCollection services)
        {
            var conn = Configuration.GetConnectionString("TrainingDatabase");
            services.AddDbContext<MachineContext>(
                builder => builder.UseSqlServer(conn,
                    o => o.EnableRetryOnFailure()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IDbInitializer paymentPortalDatabaseInitializer,
            IWebHostEnvironment env)
        {
            var productionFeature = new FeatureToggleProduction();
            if (productionFeature.FeatureEnabled)
            {
                paymentPortalDatabaseInitializer.InitializeAsync().Wait();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
