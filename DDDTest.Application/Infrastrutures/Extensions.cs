

using DDDTest.Application.User.Commands.RegisterUserCommand;
using DDDTest.Application.User.DomainServices;
using DDDTest.Application.User.Services;
using DDDTest.DataAccess.Repositories.User;
using DDDTest.Domain.Aggregates.UserAggregate.DomainServices;
using DDDTest.Domain.Aggregates.UserAggregate.Repositories;
using DDDTest.Domain.Framework.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DDDTest.Application.Infrastrutures;
public static class Extensions {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
       // AddCustomMediatR(services);
        services.AddCommands();
        // services.AddSingleton<IPackingListFactory, PackingListFactory>();



        services.AddMediatR(typeof(RegisterUserCommand).GetTypeInfo().Assembly);

       // builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

        //services.Scan(b => b.FromAssemblies(typeof(IMediator).GetTypeInfo().Assembly)
        //    .AddClasses(c => c.AssignableTo(typeof(IMediator)))
        //    .AsImplementedInterfaces()
        //    .WithScopedLifetime());



        services.AddScoped<IUserDomainServices, UserDomainServices>();
        services.AddScoped<IUserRepositoryQuery, UserRepositoryQuery>();
        services.AddScoped<IUserRepositoryCommand, UserRepositoryCommand>();



        services.AddScoped<IUserService, UserService>();
        return services;
    }
    private static IServiceCollection AddCommands(this IServiceCollection services) {
        var assembly = Assembly.GetCallingAssembly();

        services.Scan(s => s.FromAssemblies(typeof(RegisterUserCommand).GetTypeInfo().Assembly)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());


        services.Scan(s => s.FromAssemblies(typeof(RegisterUserCommandValidator).Assembly)
         .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
         .AsImplementedInterfaces()
         .WithScopedLifetime());

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

         services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }



    public static byte[] ToBytes(this IFormFile formFile) {
        // if (formFile.Length > 0) {
        using var ms = new MemoryStream();
        formFile.CopyTo(ms);
        return ms.ToArray();
        //string s = Convert.ToBase64String(fileBytes);
        // act on the Base64 data
        //}
    }
}
