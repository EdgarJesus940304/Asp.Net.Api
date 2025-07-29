using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Business.Handlers;
using WebApi.Business.Models;

namespace WebApi.Controllers
{
    public class MedicationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] MedicationModel medication)
        {
            using (var medicationHandler = new MedicationHandler())
            {
                var response = medicationHandler.SaveMedication(medication);
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

        [HttpPut]
        public HttpResponseMessage Put([FromBody] MedicationModel medication)
        {
            using (var medicationHandler = new MedicationHandler())
            {
                var response = medicationHandler.UpdateMedication(medication);
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
        public HttpResponseMessage Delete(int medicationId)
        {
            using (var medicationHandler = new MedicationHandler())
            {
                var response = medicationHandler.DeleteMedication(medicationId);
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