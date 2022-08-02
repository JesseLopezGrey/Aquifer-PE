using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Domain;
using Sabio.Models.Requests.OrganizationFollowers;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/followers")]
    [ApiController]
    public class OrganizationFollowersController : BaseApiController
    {


        private IOrganizationsFollowersServices _service = null;
        private IAuthenticationService<int> _authService = null;

        public OrganizationFollowersController(IOrganizationsFollowersServices service, ILogger<OrganizationFollowersController> logger,
            IAuthenticationService<int> authenticationService) : base(logger)
        {
            _service = service;
            _authService = authenticationService;
        }

        [HttpPost]
        public ActionResult<SuccessResponse> Create(OrganizationFollowersRequest model)
        {
            int code = 201;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.Add(model);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }



        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<OrganizationFollowers>> GetById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                OrganizationFollowers aOrgFol = _service.GetByOrganizationId(id);

                if (aOrgFol == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<OrganizationFollowers> { Item = aOrgFol };
                }
            }


            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Errors: {ex.Message}");
            }


            return StatusCode(iCode, response);
        }



    }
}
