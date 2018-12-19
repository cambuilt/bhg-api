using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bhg.Filters;
using bhg.Infrastructure;
using bhg.Interfaces;
using bhg.Models;
using bhg.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace bhg
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
            services.Configure<AppInfo>(Configuration.GetSection("Info"));
            services.AddScoped<ITreasureMapRepository, TreasureMapRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            // services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMemoryCache();
            services
                .AddMvc(options =>
                {
                    options.Filters.Add<JsonExceptionFilter>();
                    options.Filters.Add<RequireHttpsOrCloseAttribute>();
                    options.Filters.Add<LinkRewritingFilter>();
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddAutoMapper(options => options.AddProfile<MappingProfile>());

            // Insted of WithOrigins, use AllowAnyOrigin for testing.
            services.AddCors(options =>
            {
                options.AddPolicy("BackyardHiddenGems", policy => policy.WithOrigins("https://backyardhiddengems.com"));
            });

            var connection = Configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<BhgContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("BackyardHiddenGems");
            app.UseMvc();
        }
    }
}
