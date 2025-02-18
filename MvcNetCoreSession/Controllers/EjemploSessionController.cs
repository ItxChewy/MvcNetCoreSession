using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSession.Helpers;
using MvcNetCoreSession.Models;

namespace MvcNetCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SessionCollection(string accion)
        {
            if(accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota {Nombre= "Oblak", Raza="Caracol", Edad=1},
                        new Mascota {Nombre= "Chema", Raza="Delfin", Edad=4},
                        new Mascota {Nombre= "Juli", Raza="Araña", Edad=2}
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["mensaje"] = "Coleccion almacenada en Session";
                    return View();
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)
                        HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
            
        }

        public IActionResult SessionMascota(string accion)
        {
            if (accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Asane Diop";
                    mascota.Raza = "Guacamayo";
                    mascota.Edad = 23;
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    HttpContext.Session.Set("mascota", data);
                    ViewData["mensaje"] = "Mascota almacenada en Session";
                }else if(accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("mascota");
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);
                    ViewData["mascota"] = mascota;
                }
            }
            return View();
        }
        public IActionResult SessionSimple(string accion)
        {
            if(accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["mensaje"] = "Datos almacenados en Session";
                }else if (accion.ToLower() == "mostrar")
                {
                    ViewData["usuario"] = HttpContext.Session.GetString("nombre");
                    ViewData["hora"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }
    }
}
