using Blog.DTO;
using Blog.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.IServices
{
    public interface IUserService
    {
        IEnumerable GetAllRoles();

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<UserDTO>> GetAllUserAsync();

        Task<ApplicationUser> GetUser(string userName);

        void DeleteUser(ApplicationUser user);

    }
    
}
