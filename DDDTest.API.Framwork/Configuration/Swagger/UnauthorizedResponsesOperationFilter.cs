using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DDDTest.API.Framwork.Configuration.Swagger;
public class UnauthorizedResponsesOperationFilter : IOperationFilter {
    private readonly bool includeUnauthorizedAndForbiddenResponses;
    private readonly string schemeName;

    public UnauthorizedResponsesOperationFilter(bool includeUnauthorizedAndForbiddenResponses, string schemeName = "Bearer") {
        this.includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
        this.schemeName = schemeName;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context) {

        var isAuthorized = (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                            || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any())
                            && !context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

        if (!isAuthorized) return;

        #region otherWay
        // Check for authorize attribute
        //var filters = context.ApiDescription.ActionDescriptor.FilterDescriptors;
        //var metadta = context.ApiDescription.ActionDescriptor.EndpointMetadata;

        //var hasAnonymous = filters.Any(p => p.Filter is AllowAnonymousFilter) || metadta.Any(p => p is AllowAnonymousAttribute);
        //if (hasAnonymous) return;

        //var hasAuthorize = filters.Any(p => p.Filter is AuthorizeFilter) || metadta.Any(p => p is AuthorizeAttribute);
        //if (!hasAuthorize) return;
        #endregion

        if (includeUnauthorizedAndForbiddenResponses) {
            operation.Responses.TryAdd
                      ("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd
                      ("403", new OpenApiResponse { Description = "Forbidden" });
        }

        operation.Security = new List<OpenApiSecurityRequirement>
        {
               new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = schemeName,
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
            } };

    }
}


