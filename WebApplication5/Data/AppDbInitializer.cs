using Microsoft.AspNetCore.Identity;
using WebApplication5.Data.Static;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Teacher))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@ubt-uni.net";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@ubt-uni.net";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User@1234!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appTeacherEmail = "teacher@ubt-uni.net";

                var appTeacher = await userManager.FindByEmailAsync(appTeacherEmail);
                if (appTeacher == null)
                {
                    var newAppTeacher = new ApplicationUser()
                    {
                        FullName = "Application Teacher",
                        UserName = "app-teacher",
                        Email = appTeacherEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppTeacher, "Teacher@1234!");
                    await userManager.AddToRoleAsync(newAppTeacher, UserRoles.Teacher);
                }
            }
        }
    }
}
