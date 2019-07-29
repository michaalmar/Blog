using AutoMapper;
using Blog.IServices;
using System.Threading.Tasks;
using System.Web.Http;


namespace Blog.Controllers.APIControllers
{

    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }


        [Route()]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await userService.GetAllUserAsync();

                return Ok(result);
            }
            catch
            {
                return InternalServerError();
            }

        }


    }
}
