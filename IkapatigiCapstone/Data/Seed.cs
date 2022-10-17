using Microsoft.AspNetCore.Identity;
using IkapatigiCapstone.Models;

namespace IkapatigiCapstone.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.HeadAdmin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.HeadAdmin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Member))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Member));
                if (!await roleManager.RoleExistsAsync(UserRoles.HowToMod))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.HowToMod));
                if (!await roleManager.RoleExistsAsync(UserRoles.DiagnosticMod))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.DiagnosticMod));
                if (!await roleManager.RoleExistsAsync(UserRoles.ForumMod))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.ForumMod));
                if (!await roleManager.RoleExistsAsync(UserRoles.Expert))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Expert));

                //User Seeds
                //Head Admin Seed
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Users>>();
                string adminUserName = "ikapatagiHead";

                var adminUser = await userManager.FindByNameAsync(adminUserName);
                if (adminUser == null)
                {
                    var newAdminUser = new Users()
                    {
                        UserName = "Ikapatagi",
                        
                    };
                    await userManager.CreateAsync(newAdminUser, "head4dmin!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.HeadAdmin);
                }

                //Member seed
                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new Users()
                    {
                        UserName = "testapp-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        
                    };
                    await userManager.CreateAsync(newAppUser, "Member4!kapatigi");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Member);
                }

                //Forum Moderator Seed
                string fModUsername = "testForumMod";

                var fModUser = await userManager.FindByNameAsync(fModUsername);

                if (fModUser == null)
                {
                    var newfMod = new Users()
                    {
                        UserName = fModUsername
                        
                    };
                    await userManager.CreateAsync(newfMod, "forumPass@123");
                    await userManager.AddToRoleAsync(newfMod, UserRoles.HeadAdmin);
                }

                //Diagnostic Moderator Seed
                string dModUsername = "testDiagMod";

                var dModUser = await userManager.FindByNameAsync(dModUsername);

                if (dModUser == null)
                {
                    var newdMod = new Users()
                    {
                        UserName = dModUsername

                    };
                    await userManager.CreateAsync(newdMod, "diagPass@456");
                    await userManager.AddToRoleAsync(newdMod, UserRoles.DiagnosticMod);
                }
                //How To Moderator Seed
                string htModUsername = "testHowMod";

                var htModUser = await userManager.FindByNameAsync(htModUsername);

                if (htModUser == null)
                {
                    var newhtMod = new Users()
                    {
                        UserName = htModUsername

                    };
                    await userManager.CreateAsync(newhtMod, "howtoPass@789");
                    await userManager.AddToRoleAsync(newhtMod, UserRoles.HowToMod);
                }
            }
        }
    }
}
