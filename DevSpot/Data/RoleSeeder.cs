using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //We check if this role hasnt been created.
            //We want to avoid to create an admin role each time we run our application.
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                //Cehck asp.net roles db
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            //JobSekeer Role
            if (!await roleManager.RoleExistsAsync(Roles.JobSeeker))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.JobSeeker));
            }
            //Employee Role
            if (!await roleManager.RoleExistsAsync(Roles.Employeer))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Employeer));
            }
        }
    }
}
