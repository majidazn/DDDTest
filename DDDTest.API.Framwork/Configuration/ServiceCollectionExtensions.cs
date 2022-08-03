using DDDTest.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTest.API.Framwork.Configuration {
    public static class ServiceCollectionExtensions {
        public static void AddDbContextCustom(this IServiceCollection services, IConfiguration configuration) {
            //services.AddDbContext<UserBoundedContextCommand>((serviceProvider, options) => {
            //    options.use(configuration.GetConnectionString("DefaultConnection"));


            //});
           
            services.AddDbContext<UserBoundedContextCommand>(opt => opt.UseNpgsql("Server=localhost;Port=5432;Database=WriteDB;User Id=sls;Password=123;"));
            services.AddDbContext<UserBoundedContextQuery>(opt => opt.UseNpgsql("Server=localhost;Port=5432;Database=ReadDB;User Id=sls;Password=123;"));


        }
    }
}
