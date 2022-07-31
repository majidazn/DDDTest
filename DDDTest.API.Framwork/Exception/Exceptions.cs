using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.API.Framwork.ExceptionMethods {
    public static class Exceptions {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder) {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
