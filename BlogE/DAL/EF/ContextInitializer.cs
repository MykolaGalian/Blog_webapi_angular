using DAL.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DAL.Models;

namespace DAL.EF
{
    public class ContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            var userManager = new ApplicationUserManager(new CustomUserStore(db));

            var roleManager = new ApplicationRoleManager(new CustomRoleStore(db));

            var role1 = new CustomRole("user");
            var role2 = new CustomRole("moderator");
            var role3 = new CustomRole("admin");

            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            var admin = new ApplicationUser()
            {
                Email = "myko@gmail.com",
                UserName = "Myko",
                UserInfo = new UserInfo() { Name = "Myk", LastName = "Gal", Login = "Myko", UserAvatar = "mmm.png", Address = "Kyiv",  IsBlocked = false,  DateRegistration = DateTime.Now }
            };
            var result = userManager.Create(admin, "111111");

            var user = new ApplicationUser()
            {
                Email = "nobody1@mail.ru",
                UserName = "Kostya",
                UserInfo = new UserInfo() { Name = "Kos", LastName = "Leva", Login = "Kostya", UserAvatar = "kkk.png", Address = "Kyiv", IsBlocked = false, DateRegistration = DateTime.Now }
            };
            var result2 = userManager.Create(user, "222222"); // min 6 characters

            var moder = new ApplicationUser()
            {
                Email = "nobody2@mail.ru",
                UserName = "Jenya",
                UserInfo = new UserInfo() { Name = "Jen", LastName = "Dola", Login = "Jenya", UserAvatar = "jjj.png", Address = "Kyiv", IsBlocked = false, DateRegistration = DateTime.Now }
            };
            var result3 = userManager.Create(moder, "333333");
            db.SaveChanges();

            var post = new Post()
            {
                PostBody = "body",
                UserInfoId = 1,
                PostPicture = "111.png",
                DateCreate = DateTime.Now,
                Hashtags = "#hello"
            };
            db.Post_.Add(post);
            db.SaveChanges();

            var comment = new Comment() { CommentBody = "com1", UserInfoId = 1, DateCreation = DateTime.Now, PostId = 1 };
            var comment2 = new Comment() { CommentBody = "com2", UserInfoId = 1, DateCreation = DateTime.Now, PostId = 1 };

            db.Comment_.Add(comment);
            db.Comment_.Add(comment2);
            db.SaveChanges();
            
            if (result.Succeeded && result2.Succeeded && result3.Succeeded)
            {
                userManager.AddToRole(admin.Id, role3.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(moder.Id, role2.Name);
                userManager.AddToRole(user.Id, role1.Name);
            }
            db.SaveChanges();
        }

    }
}
