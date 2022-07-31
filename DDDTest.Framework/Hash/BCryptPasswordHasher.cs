using Microsoft.AspNetCore.Identity;


namespace DDDTest.Domain.Framework.Hash {
    // Adapted from ScottBrady91.AspNetCore.Identity.BCryptPasswordHasher
    // https://github.com/scottbrady91/ScottBrady91.AspNetCore.Identity.BCryptPasswordHasher
    public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class {
        public string HashPassword(TUser user, string password) {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        public PasswordVerificationResult VerifyHashedPassword(
          TUser user, string hashedPassword, string providedPassword) {
            var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

            if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, 12)) {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }

            return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
