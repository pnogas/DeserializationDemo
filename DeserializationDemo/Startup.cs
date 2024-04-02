using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace DeserializationDemo;

public class Startup
{
    private const string PathBase = "/Base";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UsePathBase(PathBase);
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        SetupFilters(services);
    }

    private static void SetupFilters(IServiceCollection services)
    {
        services.AddControllers(
                options => { options.Filters.Add(typeof(ContractValidationFilter)); })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
            });
    }
}