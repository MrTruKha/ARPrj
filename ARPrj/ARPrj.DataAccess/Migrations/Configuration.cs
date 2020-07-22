namespace ARPrj.DataAccess.Migrations
{
    using ARPrj.DataAccess.Model;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ARPrj.DataAccess.ARPrjContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ARPrj.DataAccess.ARPrjContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            CreateRoleSample(context);
            CreateUserSample(context);
        }
        private void CreateRoleSample(ARPrj.DataAccess.ARPrjContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (roleManager.Roles.Count() == 0)
            {

                var list = new List<IdentityRole>()
                {
                    new IdentityRole() {Name = "Admin"},
                    new IdentityRole() {Name = "Staff"},
                    new IdentityRole() {Name = "Customer"}
                };
                foreach (var VARIABLE in list)
                {
                    roleManager.Create(VARIABLE);
                }
            }
        }
        private void CreateUserSample(ARPrjContext context)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            if (context.Users.Count() == 0)
            {
                List<UserCommon> listUser = new List<UserCommon>()
                        {
                            new UserCommon(){UserName="Admin",FullName="Vũ Quang Trường",PasswordHash= "666888",Email="truongvq2@gmail.com"}                   
                        };
                foreach (var item in listUser)
                {
                    userManager.Create(item, item.PasswordHash);
                    if (item.UserName.Equals("Admin"))
                    {
                        userManager.AddToRole(item.Id, "Admin");
                    }
                    if (item.UserName.Contains("Staff"))
                    {
                        userManager.AddToRole(item.Id, "Staff");
                    }
                    if (item.UserName.Contains("Customer"))
                    {
                        userManager.AddToRole(item.Id, "Customer");
                    }
                }
            }
        }
    }
}
