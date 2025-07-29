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
    public class UserHandler
    {
        public MessageResponse GetUsers()
        {
            using (var Db = new Cepdi_PruebaEntities())
            {
                try
                {
                    var users = Db.usuarios.Select(us => new UserModel()
                    {
                        Id = us.idusuario,
                        UserName = us.usuario,
                        Password = us.password,
                        CreationDate = us.fechacreacion,
                        Name = us.nombre,
                        Perfil = us.idperfil,
                        Status = us.estatus
                    }).ToList();

                    return new MessageResponse()
                    {
                        ResponseType = ResponseType.OK,
                        Data = users
                    };
                }
                catch (Exception ex)
                {
                    return new MessageResponse()
                    {
                        ResponseType = ResponseType.Error,
                        Message = $"No fue posible consultar los usuarios {ex.Message} {ex?.InnerException?.Message}"
                    };
                }
            }
        }

        public MessageResponse GetUserById(int id)
        {
            using (var Db = new Cepdi_PruebaEntities())
            {
                try
                {
                    var data = Db.usuarios.FirstOrDefault(f => f.idusuario == id);

                    UserModel user = new UserModel()
                    {
                        Id = data.idusuario,
                        UserName = data.usuario,
                        Password = data.password,
                        CreationDate = data.fechacreacion,
                        Name = data.nombre,
                        Perfil = data.idperfil,
                        Status = data.estatus
                    };

                    return new MessageResponse()
                    {
                        ResponseType = ResponseType.OK,
                        Data = user
                    };
                }
                catch (Exception ex)
                {
                    return new MessageResponse()
                    {
                        ResponseType = ResponseType.Error,
                        Message = $"No fue posible obtener el usuario especificado {ex.Message} {ex?.InnerException?.Message}"
                    };
                }
            }
        }

        public MessageResponse SaveUser(UserModel user)
        {
            using (var Db = new Cepdi_PruebaEntities())
            {
                try
                {
                    Db.usuarios.Add(new usuarios()
                    {
                        nombre = user.Name,
                        estatus = user.Status,
                        fechacreacion = DateTime.Now,
                        usuario = user.UserName,
                        password = user.Password,
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
                        Message = $"No fue posible registrar el usuario capturado {ex.Message} {ex?.InnerException?.Message}"
                    };
                }
            }
        }

        public MessageResponse UpdateUser(UserModel user)
        {
            using (var Db = new Cepdi_PruebaEntities())
            {
                try
                {
                    var entry = Db.usuarios.Find(user.Id);

                    if (entry == null)
                    {
                        return new MessageResponse()
                        {
                            ResponseType = ResponseType.Error,
                            Message = $"No fue posible obtener el usuario especificado"
                        };
                    }


                    Db.Entry(entry).CurrentValues.SetValues(user);
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
                        Message = $"No fue posible actualizar el usuario especificado {ex.Message} {ex?.InnerException?.Message}"
                    };
                }
            }
        }

        public MessageResponse DeleteUser(UserModel user)
        {
            using (var Db = new Cepdi_PruebaEntities())
            {
                try
                {
                    var entry = Db.usuarios.Find(user.Id);

                    if (entry == null)
                    {
                        return new MessageResponse()
                        {
                            ResponseType = ResponseType.Error,
                            Message = $"No fue posible obtener el usuario especificado"
                        };
                    }


                    Db.usuarios.Remove(entry);
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
                        Message = $"No fue posible eliminar el usuario especificado {ex.Message} {ex?.InnerException?.Message}"
                    };
                }
            }
        }
    }
}
