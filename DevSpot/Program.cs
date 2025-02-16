using DevSpot.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevSpot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDBContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
            });
            builder.Services.AddDefaultIdentity<IdentityUser>(options => 
            { 
                options.SignIn.RequireConfirmedAccount = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Creating the ADMIN Role
            using (var scope= app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager= services.GetRequiredService<RoleManager<IdentityRole>>();

                //We check if this role hasnt been created.
                //We want to avoid to create an admin role each time we run our application.
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    //Cehck asp.net roles db
                    var result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;
                }
            }

                app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
