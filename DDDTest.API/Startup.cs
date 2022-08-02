using DDDTest.API.Framwork.Configuration;
using DDDTest.API.Framwork.Configuration.Swagger;
using DDDTest.Application.Infrastrutures;
using System.Reflection;
using FluentValidation;
using DDDTest.API.Framwork.ExceptionMethods;
using EventStore.Client;
using EventStore.ClientAPI;

namespace DDDTest.API;
public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services) {
       // services.AddShared();
        services.AddApplication();
        //services.AddInfrastructure(Configuration);





        services.AddEventStore();


        services.AddControllers();
        services.AddDbContextCustom(Configuration);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        services.SwaggerSetup();




   
    }

 

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerCustom();
        }
        app.UseCustomExceptionHandler();
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

