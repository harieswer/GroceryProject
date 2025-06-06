using ApplicationCore.Command;
using ApplicationCore.Models;
using ApplicationCore.Quey;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UtilityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register-customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegisterModel customerRegisterModel)
        {
            int result = await _mediator.Send(new CustomerRegisterRequestParameter() { CustomerRegisterModel = customerRegisterModel });
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("customer-login")]
        public async Task<IActionResult> CustomerSignin([FromBody] CustomerLoginModel customerLoginModel)
        {
            ApplicationCore.Responses.CustomerSignInResponse result = await _mediator.Send(new CustomerSignInRequestQueryParamter() { Email = customerLoginModel.Email, Password = customerLoginModel.Password });
            return Ok(result);
        }
    }

}
