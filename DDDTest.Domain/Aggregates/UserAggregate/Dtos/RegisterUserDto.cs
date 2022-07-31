using Microsoft.AspNetCore.Http;

namespace DDDTest.Domain.Aggregates.UserAggregate.Dtos;
public class RegisterUserDto {
 
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public IFormFile? Avatar { get; set; }
}

