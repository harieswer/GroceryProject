using ApplicationCore.Quey;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UtilityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("get-userdetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            return Ok(await _mediator.Send(new UserDetailsRequest()));
        }

        [HttpGet("get-userdetailsbyid/{userid}")]
        public async Task<IActionResult> GetUserDetailsid([FromRoute] int userid)
        {
            return Ok(await _mediator.Send(new UserDetailsRequestParamater() { UserId = userid }));
        }

    }
}
