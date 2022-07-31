using Microsoft.AspNetCore.Identity;

namespace DDDTest.Domain.Framework.Hash;
public interface IPasswordHasher<TUser> where TUser : class {
    string HashPassword(TUser user, string password);

    PasswordVerificationResult VerifyHashedPassword(
      TUser user, string hashedPassword, string providedPassword);
}

