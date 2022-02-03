using Experian.API.Business;
using Experian.API.Controllers;
using Experian.API.Helpers;
using Experian.API.Helpers.RestClient;
using Experian.API.Integration;
using Experian.API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experian.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Experian.API", Version = "v1" });
            });
            services.Configure<Configuration>(Configuration);
            services.AddLogging();
            services.AddHttpClient();

            services.AddScoped<IRestClientHelper, RestClientHelper>();
            services.AddScoped<IPhotoAlbumIntegration>(a => new PhotoAlbumIntegration(a.GetService<IRestClientHelper>(), a.GetService<IOptions<Configuration>>()));
            services.AddScoped<IPhotoAlbumBusiness>(a => new PhotoAlbumBusiness(a.GetService<IPhotoAlbumIntegration>()));
            services.AddScoped(a => new PhotoAlbumController(a.GetService<ILogger<PhotoAlbumController>>(), a.GetService<IPhotoAlbumBusiness>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Experian.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
