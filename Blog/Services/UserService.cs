using Blog.App_Start;
using Blog.DAL;
using Blog.DTO;
using Blog.IServices;
using Blog.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Services
{
    public class UserService : IUserService
    {

        private readonly BlogContext dbContext;
        private readonly ApplicationUserManager userManager;


        public UserService(BlogContext dbContext, ApplicationUserManager userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUserAsync()
        {
            var userList = dbContext.Users;

            return await userList.ToListAsync();

        }


        public IEnumerable GetAllRoles()
        {
            var rolesList = new SelectList(dbContext.Roles.Where(u => !u.Name.Contains(Constant.Constant.User.ROLE_ADMIN)).ToList(), "Name", "Name");
            return rolesList;

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await dbContext.SaveChangesAsync()) > 0;
        }
        
    }
}