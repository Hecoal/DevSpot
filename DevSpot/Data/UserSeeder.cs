using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class UserSeeder
    {
        public static async Task SeedUserAsync (IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await CreateUserWithRole(userManager, "adming@gmail.com", "Admin123!", Roles.Admin);
            await CreateUserWithRole(userManager, "jobseeker@gmail.com", "JobSeeker123!", Roles.JobSeeker);
            await CreateUserWithRole(userManager, "employeer@gmail.com", "Employeer123!", Roles.Employeer);

        }

        private static async Task CreateUserWithRole(UserManager<IdentityUser>userManager, string email, string password, string role)
        {
            //If user hasnt been created
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);

                }
                else
                {
                    throw new Exception($"Failed creating user with Email {user.Email}. Errors: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
