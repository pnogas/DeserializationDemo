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
        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ContractValidationFilter));
                // options.ModelBinderProviders.Insert(0, new BaseSettingModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new DemoContract2ModelBinderProvider());
            })
            // Change TypeNameHandling to reproduce exploit
            .AddNewtonsoftJson(options => { options.SerializerSettings.TypeNameHandling = TypeNameHandling.None; });
        
    }
}