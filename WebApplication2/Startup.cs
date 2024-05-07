using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApplication2.Interface;
using WebApplication2.Models.Data;
using WebApplication2.Models.Mocks;

public class Startup
{

    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = _configuration.GetConnectionString("MyDbConnection");

        services.AddDbContext<AvtoStrachovanieDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddTransient<IBank, MockBank>();
        services.AddTransient<IAllInsurancePackages, MockAllInsurancePackages>();
        services.AddTransient<IBankReviews, MockBankReviews>();
        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AvtoStrachovanie", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "AvtoStrachovanie V1");
            c.RoutePrefix = string.Empty;
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
    }

}

