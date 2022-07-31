using DDDTest.Application.User.Commands.RegisterUserCommand;
using DDDTest.Application.User.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace DDDTest.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        #region Fields

        private readonly IMediator _mediator;
        private readonly IUserService userService;

        #endregion

        #region Constructor
        public UserController(IMediator mediator, IUserService userService) {
            _mediator = mediator;
            this.userService = userService;
        }
        #endregion


        #region Methods

        [DisplayName("Register User")]
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateCenter([FromForm] RegisterUserCommand registerUserCommand, CancellationToken cancellationToken = default)
        => Ok(await _mediator.Send(registerUserCommand, cancellationToken));







        #endregion
    }
}
