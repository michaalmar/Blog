using AutoMapper;
using Blog.DTO;
using Blog.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace Blog.Controllers
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

                var mappedResult = mapper.Map<IEnumerable<UserDTO>>(result);
                return Ok(mappedResult);
            }
            catch
            {
                return InternalServerError();
            }

        }


    }
}
