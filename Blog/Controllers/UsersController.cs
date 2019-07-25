using Blog.IServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace Blog.Controllers
{

    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }


        [Route]
        public async Task<IHttpActionResult> Get()
        {

            try
            {


            }
            catch
            {

            }

            return InternalServerError();
        }
      

    }
}
