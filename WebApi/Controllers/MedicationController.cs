﻿using System;
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
    public class MedicationController : ApiController
    {

        [HttpPost]
        [Route("api/medications/list")]
        public HttpResponseMessage GetMedications([FromBody] FilterDataTableModel filterData)
        {
            using (var medicationHandler = new MedicationHandler())
            {
                var response = medicationHandler.GetMedications(filterData);

                HttpStatusCode statusCode = response.ResponseType == Business.Utils.ResponseType.OK
                         ? HttpStatusCode.OK
                         : HttpStatusCode.InternalServerError;

                return Request.CreateResponse(statusCode, new
                {
                    response.Data
                });
            }
        }


        [HttpGet]
        [Route("api/medications/{id}")]
        public HttpResponseMessage GetUser(int id)
        {
            using (var medicationHandler = new MedicationHandler())
            {
                var response = medicationHandler.GetMedicationById(id);

                HttpStatusCode statusCode = response.ResponseType == Business.Utils.ResponseType.OK
                    ? HttpStatusCode.OK
                    : HttpStatusCode.InternalServerError;

                return Request.CreateResponse(statusCode, new MessageResponse
                {
                    ResponseType = response.ResponseType,
                    Message = response.Message,
                    Data = response.Data
                });
            }
        }


        [HttpPost]
        [Route("api/medications")]
        public HttpResponseMessage Post([FromBody] MedicationModel medication)
        {
            using (var medicationHandler = new MedicationHandler())
            {
                var response = medicationHandler.SaveMedication(medication);

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
        [Route("api/medications/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] MedicationModel medication)
        {
            using (var medicationHandler = new MedicationHandler())
            {
                var response = medicationHandler.UpdateMedication(medication);

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

        [HttpDelete]
        [Route("api/medications/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            using (var medicationHandler = new MedicationHandler())
            {
                var response = medicationHandler.DeleteMedication(id);

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
    }
}