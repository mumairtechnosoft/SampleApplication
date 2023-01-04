using Feedback360.DB;
using Feedback360.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace SampleApplication.Common
{
    public static class SeedData
    {
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                #region Role

                string[] roles = new string[] { "Manager", "Employee" };

                var newrolelist = new List<Role>();
                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        Role newrole = new Role { Name = role, Created_By = "System", Created_Date = DateTime.Now, Modified_By = "System", Modified_Date = DateTime.Now, Deleted = false };
                        newrolelist.Add(newrole);
                    }
                }
                context.Roles.AddRange(newrolelist);

                #endregion

                context.SaveChanges();
            }
        }

        public static void SeedUsers(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                #region User

                var users = new List<User>
                {
                    new User
                    {
                        First_Name = "Ali",
                        Last_Name = "Ahmad",
                        Username = "aahmad",
                        Email = "aahmad@techno-soft.com",
                        Password = "Ts123456!",
                        Confirm_Password = "Ts123456!",
                        Role = context.Roles.Where(x => x.Name == "Manager").FirstOrDefault().Id,
                        Created_By = "System",
                        Created_Date = DateTime.Now,
                        Modified_By = "System",
                        Modified_Date = DateTime.Now,
                        Deleted = false
                    },
                    new User
                    {
                        First_Name = "Amir",
                        Last_Name = "Abbas",
                        Username = "aabbas",
                        Email = "aabbas@techno-soft.com",
                        Password = "Ts123456!",
                        Confirm_Password = "Ts123456!",
                        Role = context.Roles.Where(x => x.Name == "Employee").FirstOrDefault().Id,
                        Created_By = "System",
                        Created_Date = DateTime.Now,
                        Modified_By = "System",
                        Modified_Date = DateTime.Now,
                        Deleted = false
                    },
                    new User
                    {
                        First_Name = "Muhammad",
                        Last_Name = "Naeem",
                        Username = "mnaeem",
                        Email = "mnaeem@techno-soft.com",
                        Password = "Ts123456!",
                        Confirm_Password = "Ts123456!",
                        Role = context.Roles.Where(x => x.Name == "Employee").FirstOrDefault().Id,
                        Created_By = "System",
                        Created_Date = DateTime.Now,
                        Modified_By = "System",
                        Modified_Date = DateTime.Now,
                        Deleted = false
                    },
                    new User
                    {
                        First_Name = "Ali",
                        Last_Name = "Hassan",
                        Username = "ahassan",
                        Email = "ahassan@techno-soft.com",
                        Password = "Ts123456!",
                        Confirm_Password = "Ts123456!",
                        Role = context.Roles.Where(x => x.Name == "Employee").FirstOrDefault().Id,
                        Created_By = "System",
                        Created_Date = DateTime.Now,
                        Modified_By = "System",
                        Modified_Date = DateTime.Now,
                        Deleted = false
                    }
                };

                var newUsers = new List<User>();
                foreach (var user in users)
                {
                    if (!context.Users.Any(x => x.Username == user.Username))
                    {
                        newUsers.Add(user);
                    }
                }
                context.Users.AddRange(newUsers);

                #endregion

                context.SaveChanges();
            }
        }
    }
}
