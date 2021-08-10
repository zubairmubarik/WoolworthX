using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyDemoProject001.Application;
using MyDemoProject001.Application.Common.Interfaces;
using MyDemoProject001.Application.Common.Models;
using MyDemoProject001.Infrastructure;
using MyDemoProject001.WebAPI.Extensions;

namespace MyDemoProject001
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "CorsPolicy";
        readonly string LogFileForTracing = "Logs/WEBAPI-{Date}.txt";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure();
            services.AddApplicationInsightsTelemetry();
            // Configuration Options
            // Database Settings
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));

            // Configuration Settings
            services.Configure<ServiceEndPoints>(Configuration.GetSection(nameof(ServiceEndPoints)));
            //services.Configure<ServiceEndPoints>(
            //    o =>
            //    {
            //        o.ClientServiceEndpoint = Configuration.GetValue<String>("ClientServiceEndpoint");
            //        o.ClientServiceEndpointToken = Configuration.GetValue<String>("ClientServiceEndpointToken");      
            //    });

            services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IServiceEndPoints>(x => x.GetRequiredService<IOptions<ServiceEndPoints>>().Value);
            

            services.AddHttpContextAccessor();

            //services.AddSingleton //         
            //services.AddTransient //                  
            //services.AddScoped //

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.AllowCredentials()
                        );
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "My Demo Project For WooliesX";
                configure.Description = "WooliesX";
                configure.Version = "v1";

                configure.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "WooliesX API";
                    document.Info.Description = "A Demp ASP.NET Core web API For WooliesX (NET 💜 Azure)";
                    document.Info.TermsOfService = "This project is just to demonstrate skills";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Zubair Mubarik",
                        Email = "zubair.mubarik@outlook.com",
                        Url = "au.linkedin.com/in/zubairmubarik/"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // By Zubair             
            // Dev environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();         

                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                // Non Dev environemnt
                app.UseExceptionHandler("/error");
            }

            // Run in both Dev & prod enironment only 
            app.UseOpenApi();
            app.UseSwaggerUi3();
          
            app.UseCors(MyAllowSpecificOrigins); //Use Cors

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            loggerFactory.AddFile(LogFileForTracing); // Log File with Date for Tracing

            // By Zubair             
            // New v1 router
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("api", "api/v1/{controller}/{action}/");
                endpoints.MapControllers();
            });            
        }
    }
}
