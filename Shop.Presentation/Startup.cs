using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shop.Application;
using Shop.Application.Mappings;
using Shop.Application.Mappings.UserMappings;
using Shop.Application.Utilities;
using Shop.IOC;
using Shop.IOC.Configurations;

namespace Shop.Presentation
{
    public class Startup
    {
        private readonly SiteSetting _siteSetting;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            _siteSetting = Configuration.GetSection(nameof(SiteSetting)).Get<SiteSetting>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSetting>(Configuration.GetSection(nameof(SiteSetting)));
            
            services.AddDbContext(Configuration);
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.AddControllers(options =>
            {
                options.Filters.Add<RequestResultFilterAttribute>();
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop.Presentation", Version = "v1" });
            });

            services.AddCustomIdentityConfiguration(_siteSetting.IdentitySetting);
            services.AddJwtAuthentication(_siteSetting.JwtSetting);
            services.RegisterGenericMapper();
            
            GenericMapperRegistry.RegisterAll();
            
            DependencyContainer.RegisterServices(services);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddServices();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.Presentation v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}