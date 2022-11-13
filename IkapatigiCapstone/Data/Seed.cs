//using AutoMapper.Execution;
//using IkapatigiCapstone.Models;
//using System.Composition;

//namespace IkapatigiCapstone.Data
//{
//    public class Seed
//    {
//        public static void SeedData(IApplicationBuilder applicationBuilder)
//        {
//            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

//                if (!context.Users.Any())
//                {
//                    context.Users.AddRange(new User() 
//                    {
//                      Email = "gingerplaid@gmail.com",
//                      PasswordHash = "gingerbaker"
//                    })
//                }
//            }

//        }
//    }
//}
////Username/Email for members: gingerplaid @gmail.com
////Password: gingerbaker
////Roles: Member


////Username: pinkgoose @yahoo.com
////Password: pinkfootedgoose
////Roles: Expert Gardener


////Username: James.Gosling
////Password: java2004
////Roles: DiagnosticMod

////Username: Ada.Lovelace
////Password: augustaking
////Roles: HowTosMod

////Username: Jessica.Jones
////Password: aliasinvestigations
////Roles: ForumsMod

////Username: StephenVincent.Strange
////Password: neurosurgeon
////Roles: HeadAdmin