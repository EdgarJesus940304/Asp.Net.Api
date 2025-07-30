using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Business.Handlers;
using WebApi.Business.Models;
using WebApi.Business.Utils;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/users/list")]
        public HttpResponseMessage GetUsers([FromBody] FilterDataTableModel filterData)
        {
            using (var userHandler = new UserHandler())
            {
                var response = userHandler.GetUsers(filterData);

                HttpStatusCode statusCode = response.ResponseType == Business.Utils.ResponseType.OK
                         ? HttpStatusCode.OK
                         : HttpStatusCode.InternalServerError;

                return Request.CreateResponse(statusCode, new
                {
                    response.Data
                });
            }
        }


        [HttpPost]
        [Route("api/users")]
        public HttpResponseMessage SaveUser([FromBody] UserModel user)
        {
            using (var userHandler = new UserHandler())
            {
                var response = userHandler.SaveUser(user);

                HttpStatusCode statusCode = response.ResponseType == Business.Utils.ResponseType.OK
                    ? HttpStatusCode.OK
                    : HttpStatusCode.InternalServerError;

                return Request.CreateResponse(statusCode, new MessageResponse
                {
                    ResponseType = response.ResponseType,
                    Message = response.Message
                });
            }
        }

        [HttpPut]
        [Route("api/users/{id}")]
        public HttpResponseMessage UpdateUser(int id, [FromBody] UserModel user)
        {
            using (var userHandler = new UserHandler())
            {
                var response = userHandler.UpdateUser(user);
                if (response.ResponseType == Business.Utils.ResponseType.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        success = true,
                        message = response.Message
                    });
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, response.Message);
                }
            }
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public HttpResponseMessage DeleteUser(int id)
        {
            using (var userHandler = new UserHandler())
            {
                var response = userHandler.DeleteUser(id);
                if (response.ResponseType == Business.Utils.ResponseType.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        success = true,
                        message = response.Message
                    });
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, response.Message);
                }
            }
        }
    }
}