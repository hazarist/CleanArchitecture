using AutoMapper;
using CelanArchitecture.Core.DTOs;
using CelanArchitecture.Core.Interfaces;
using CelanArchitecture.Core.Requests;
using CelanArchitecture.ServiceProcessors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Valido.MobileBackend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IServiceProcessor serviceProcessor;
        public UserController(IUserService userService, IMapper mapper, IServiceProcessor serviceProcessor)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.serviceProcessor = serviceProcessor;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Bad request");
            }

            var serviceRepsonse = serviceProcessor.Call(userService.Authenticate, request.Username, request.Password);

            if (!serviceRepsonse.Succeeded)
            {
                return Json(serviceRepsonse);
            }
            serviceRepsonse.Content = mapper.Map<UserDto>(serviceRepsonse.Content);
            return Json(serviceRepsonse);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public IActionResult Registration([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Bad request");
            }

            var serviceRepsonse = serviceProcessor.Call(userService.CreateUser, request);
            if (!serviceRepsonse.Succeeded)
            {
                return Json(serviceRepsonse);
            }
            serviceRepsonse.Content = mapper.Map<UserDto>(serviceRepsonse.Content);
            return Json(serviceRepsonse);
        }
    }
}
