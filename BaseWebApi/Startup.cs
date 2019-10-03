using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using prioritizemeServices.Database;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Threading;

namespace prioritizemeServices
{
    /// <summary>
    /// The Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The <see cref="IConfiguration"/>
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The CORS policy for origins allowed by this API
        /// </summary>
        private const string MyAllowSpecificOrigins = "_myAllowSepecificOrigins";

        /// <summary>
        /// The setting name of the SQL Connection string
        /// </summary>
        public const string SqlConnectionSettingName = "PrioritizeMeConn";

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Injected <see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Thread settings for .net-core: https://docs.microsoft.com/en-us/azure/redis-cache/cache-faq#important-details-about-threadpool-growth
            ThreadPool.SetMinThreads(400, 400);

            services.AddCors(opts =>
            {
                opts.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins(
                                "http://localhost:3000",
                                "https://prioritizemylife.com")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add Entity Framework
            string connectionString = Configuration[$"ConnectionStrings:{SqlConnectionSettingName}"];
            services.AddScoped<
                IDesignTimeDbContextFactory<PrioritizeMeDbContext>,
                DesignTimePrioritizedListDbContextFactory>(provider =>
                    new DesignTimePrioritizedListDbContextFactory(connectionString));

            // Add Swagger
            string pathToDoc = "BaseWebApi.xml";
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Base API",
                        Description = "The main base API service",
                        TermsOfService = "None"
                    });

                string filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToDoc);
                options.IncludeXmlComments(filePath);
                options.DescribeAllEnumsAsStrings();
            });

            // Custom injectors

            // Add ApplicationInsights
            services.AddApplicationInsightsTelemetry(Configuration);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Injected <see cref="IApplicationBuilder"/></param>
        /// <param name="env">Injected <see cref="IHostingEnvironment"/></param>
        /// <param name="contextFactory">The application <see cref="IDesignTimeDbContextFactory{TContext}"/></param>
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IDesignTimeDbContextFactory<PrioritizeMeDbContext> contextFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();

                // Automatically migrate the application to the latest
                using (PrioritizeMeDbContext migrationContext = contextFactory.CreateDbContext(new string[0]))
                {
                    migrationContext.Database.Migrate();
                }
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
        }
    }
}
