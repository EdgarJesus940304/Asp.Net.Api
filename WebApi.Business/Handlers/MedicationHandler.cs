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
    public class MedicationHandler
    {
        public MessageResponse GetMedications()
        {
            using (var Db = new Cepdi_PruebaEntities())
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
        }

        public MessageResponse GetMedicationById(int id)
        {
            using (var Db = new Cepdi_PruebaEntities())
            {
                try
                {
                    var data = Db.medicamentos.FirstOrDefault(f => f.idmedicamento == id);

                    MedicationModel medication = new MedicationModel()
                    {
                        Id = data.idmedicamento,
                        Name = data.nombre,
                        Concentration = data.concentracion,
                        Presentation = data.presentacion,
                        Price = data.precio,
                        Stock = data.stock,
                        Enable = data.bhabilitado, 
                        PharmaceuticalForm = new PharmaceuticalFormModel()
                        {
                            Id = id,
                            Name = data.formasfarmaceuticas.nombre
                        }
                    };

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
        }

        public MessageResponse SaveMedication(MedicationModel medication)
        {
            using (var Db = new Cepdi_PruebaEntities())
            {
                try
                {
                    Db.medicamentos.Add(new medicamentos()
                    {
                        nombre = medication.Name,
                        concentracion = medication.Concentration,
                        presentacion = medication.Presentation,
                        precio = medication.Price,
                        stock = medication.Stock,
                        bhabilitado = medication.Enable,
                        idformafarmaceutica = medication?.PharmaceuticalForm?.Id
                    });

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
        }

        public MessageResponse UpdateMedication(MedicationModel medication)
        {
            using (var Db = new Cepdi_PruebaEntities())
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
        }

        public MessageResponse DeleteUser(MedicationModel medication)
        {
            using (var Db = new Cepdi_PruebaEntities())
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
}
