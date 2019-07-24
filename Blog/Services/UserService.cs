using Blog.DAL;
using Blog.IServices;
using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace Blog.Services
{
    public class UserService : IUserService
    {

        private readonly BlogContext dbContext;

        public UserService(BlogContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable GetRoles()
        {
            var rolesList = new SelectList(dbContext.Roles.Where(u => !u.Name.Contains(Constant.Constant.User.ROLE_ADMIN)).ToList(), "Name", "Name");
            return rolesList;

        }
    }
}