using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Business.Models;
using WebApi.Business.Utils;
using WebApi.Data;

namespace WebApi.Business.Handlers
{
    public class MedicationHandler : BaseHandler
    {
        public MessageResponse GetMedications()
        {
            try
            {
                var medications = Db.medicamentos.Select(md => new MedicationModel()
                {
                    Id = md.idmedicamento,
                    Name = md.nombre,
                    Concentration = md.concentracion,
                    Presentation = md.presentacion,
                    Price = md.precio,
                    Stock = md.stock,
                    Enable = md.bhabilitado
                }).ToList();

                return new MessageResponse()
                {
                    ResponseType = ResponseType.OK,
                    Data = medications
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse()
                {
                    ResponseType = ResponseType.Error,
                    Message = $"No fue posible consultar los medicamentos {ex.Message} {ex?.InnerException?.Message}"
                };
            }
        }

        public MessageResponse GetMedicationById(int id)
        {
            try
            {
                var data = Db.medicamentos.FirstOrDefault(f => f.idmedicamento == id);
                MedicationModel medication = data.ToMedicationBusiness();

                return new MessageResponse()
                {
                    ResponseType = ResponseType.OK,
                    Data = medication
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse()
                {
                    ResponseType = ResponseType.Error,
                    Message = $"No fue posible obtener el medicamento especificado {ex.Message} {ex?.InnerException?.Message}"
                };
            }
        }

        public MessageResponse SaveMedication(MedicationModel medication)
        {
            try
            {
                Db.medicamentos.Add(medication.ToMedicationData());
                Db.SaveChanges();

                return new MessageResponse()
                {
                    ResponseType = ResponseType.OK
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse()
                {
                    ResponseType = ResponseType.Error,
                    Message = $"No fue posible guardar el medicamento capturado {ex.Message} {ex?.InnerException?.Message}"
                };
            }
        }

        public MessageResponse UpdateMedication(MedicationModel medication)
        {
            try
            {
                var entry = Db.medicamentos.Find(medication.Id);

                if (entry == null)
                {
                    return new MessageResponse()
                    {
                        ResponseType = ResponseType.Error,
                        Message = $"No fue posible obtener el medicamento especificado"
                    };
                }


                Db.Entry(entry).CurrentValues.SetValues(medication);
                Db.SaveChanges();

                return new MessageResponse()
                {
                    ResponseType = ResponseType.OK
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse()
                {
                    ResponseType = ResponseType.Error,
                    Message = $"No fue posible actualizar el medicamento especificado {ex.Message} {ex?.InnerException?.Message}"
                };
            }
        }

        public MessageResponse DeleteMedication(int medicationId)
        {
            try
            {
                var entry = Db.medicamentos.Find(medicationId);

                if (entry == null)
                {
                    return new MessageResponse()
                    {
                        ResponseType = ResponseType.Error,
                        Message = $"No fue posible obtener el medicamento especificado"
                    };
                }


                Db.medicamentos.Remove(entry);
                Db.SaveChanges();

                return new MessageResponse()
                {
                    ResponseType = ResponseType.OK
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse()
                {
                    ResponseType = ResponseType.Error,
                    Message = $"No fue posible eliminar el medicamento especificado {ex.Message} {ex?.InnerException?.Message}"
                };
            }

        }
    }
}
