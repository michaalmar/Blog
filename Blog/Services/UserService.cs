using Blog.App_Start;
using Blog.DAL;
using Blog.DTO;
using Blog.IServices;
using System.Collections;
using System.Collections.Generic;
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

        public async Task<IEnumerable<UserDTO>> GetAllUserAsync()
        {

            var usersWithRoles = (from user in dbContext.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,                                   
                                      RoleNames = (from userRole in user.Roles
                                                   join role in dbContext.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserDTO()

                                  {
                                     UserId = p.UserId,
                                     UserName= p.Username,   
                                     Role = string.Join(",", p.RoleNames)
                                  });

            return usersWithRoles;

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