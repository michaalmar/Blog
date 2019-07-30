using AutoMapper;
using Blog.IServices;
using System;
using System.Net.Http;
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

        [Route("{userName}")]
        [HttpDelete]
        public async Task<HttpResponseMessage>Delete(string userName)
        {
            try
            {
                if(userName == "admin")
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "You can't delete admin user");
                }
                var user = await userService.GetUser(userName);

                if(user ==null)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, $"No user with name: {userName}");
                }
                userService.DeleteUser(user);
                if(await userService.SaveChangesAsync())
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, $"User {userName} deleted");
                }
                else
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, $"Can't delete user");
                }

            }
            catch(Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, $"Can't delete user, please contact administrator");
            }
        }
    


    }
}
