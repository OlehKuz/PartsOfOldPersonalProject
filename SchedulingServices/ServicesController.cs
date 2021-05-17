using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Enums;
using Common.Helpers;
using IdentityServer4.Extensions;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulingServices.Domain.DTO;

namespace SchedulingServices
{

    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;
        private readonly IAuthorizationService _authorizationService;
        public ServicesController(IServicesManager servicesManager, IAuthorizationService authorizationService)
        {
            _servicesManager = servicesManager;
            _authorizationService = authorizationService;
        }

        [ProducesResponseType(typeof(PagingResponse<ServiceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("Many")]
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery] PagingQuery pagingQuery, [FromQuery] IEnumerable<long> ids = null)
        {
            Console.WriteLine("User IsAuthenticated SchedulingServices " + User.IsAuthenticated());
            /*
             * if my paging query is a struct it fails right before even achieving this block of controller action code 
             * 
             *  Could not create an instance of type 'Infrastructure.DataAccess.PagingQuery'. Model bound complex types must not
             * be abstract or value types and must have a parameterless constructor. Alternatively, give the 'pagingQuery'
             * parameter a non-null default value.
             */
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            //var result = await _servicesManager.GetServicesAsync(pagingQuery, ids, HttpContext.RequestAborted);
            // return Ok(result);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] long id)
        {
            var result = await _servicesManager.GetAsync(id, CancellationToken.None);
            return Ok(result);
        }

        //[Authorize(Policies.BeautyMasterPolicy)]
        [ProducesResponseType(typeof(ServiceDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ServicePostDto servicePostDto)
        {
            var allowed = await _authorizationService.AuthorizeAsync(User, Policies.BeautyMasterPolicy);

            var role = User.IsInRole(EnumExtentions.GetEnumValueName(Role.BeautyMasterRole));
            var role1 = User.IsInRole(EnumExtentions.GetEnumValueName(Role.CustomerRole));
            if (!allowed.Succeeded) return Challenge();
            var result = await _servicesManager.PostAsync(servicePostDto);
            return Ok(result);
        }

        // [Authorize(Policies.CustomerPolicy)]
        [Route("Role")]
        [ProducesResponseType(typeof(ServiceDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allowed = await _authorizationService.AuthorizeAsync(User, Policies.CustomerPolicy);

            var role = User.IsInRole(EnumExtentions.GetEnumValueName(Role.BeautyMasterRole));
            var role1 = User.IsInRole(EnumExtentions.GetEnumValueName(Role.CustomerRole));
            if (!allowed.Succeeded) return Challenge();
            var here = "we goo to CustomerPolicy method";
            return Ok(here);
        }
    }
}