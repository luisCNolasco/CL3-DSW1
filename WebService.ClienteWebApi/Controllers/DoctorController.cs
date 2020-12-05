using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using WebService.Data.Model;
namespace WebService.ClienteWebApi.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public List<SP_LISTAR_DOCTOR_Result>ListDoctor()
        {
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("http://localhost/ApiCL3/");
        
            var request = clienteHttp.GetAsync("api/Doctor").Result;

            var rs =request.Content.ReadAsStringAsync().Result;
            var misDoctores = JsonConvert.DeserializeObject<List<SP_LISTAR_DOCTOR_Result>>(rs);
            return misDoctores.ToList();
        }

        public ActionResult ListadoDoctores()
        {
            return View(ListDoctor());
        }

        public List<SP_LISTAR_DISTRITO_Result> ListDistrito()
        {
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("http://localhost/ApiCL3/");

            var request = clienteHttp.GetAsync("api/Distrito").Result;

            var rs = request.Content.ReadAsStringAsync().Result;
            var distritos = JsonConvert.DeserializeObject<List<SP_LISTAR_DISTRITO_Result>>(rs);
            return distritos.ToList();
        }
        public List<SP_LISTAR_ESPECIALIDAD_Result> ListEspecialidad()
        {
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("http://localhost/ApiCL3/");

            var request = clienteHttp.GetAsync("api/Especialidad").Result;

            var rs = request.Content.ReadAsStringAsync().Result;
            var especialidades = JsonConvert.DeserializeObject<List<SP_LISTAR_ESPECIALIDAD_Result>>(rs);
            return especialidades.ToList();
        }

        [HttpGet]
        public ActionResult nuevoDoctor()
        {
            ViewBag.distrito = new SelectList(ListDistrito(), "IDE_DIS", "NOM_DIS");
            ViewBag.especialidad = new SelectList(ListEspecialidad(), "IDE_ESP", "DES_ESP");
            return View(new DOCTOR());
        }
        [HttpPost]
        public ActionResult nuevoDoctor(DOCTOR obj)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.distrito = new SelectList(ListDistrito(), "IDE_DIS", "NOM_DIS");
                ViewBag.especialidad = new SelectList(ListEspecialidad(), "IDE_ESP", "DES_ESP");
                return View(obj);
            }

            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress= new Uri("http://localhost/ApiCL3/");

            var request = clienteHttp.PostAsync("api/Doctor",obj,new JsonMediaTypeFormatter()).Result;

            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<int>(resultString);
                if (estado == 1)
                {

                    ViewBag.distrito = new SelectList(ListDistrito(), "IDE_DIS", "NOM_DIS");
                    ViewBag.especialidad = new SelectList(ListEspecialidad(), "IDE_ESP", "DES_ESP");
                    ViewBag.mensaje = estado + " Empleado registrado correctamente..!!";
                    return View(obj);
                }
                return View(obj);
            }
            return View();
        }

        [HttpGet]
        public ActionResult actualizarDoctor(int id)
        {
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("http://localhost/ApiCL3/");
            var request = clienteHttp.GetAsync("api/Doctor?id=" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<DOCTOR>(resultString);
                ViewBag.especialidad = new SelectList(ListEspecialidad(), "IDE_ESP", "DES_ESP", obj.IDE_ESP);
                ViewBag.distrito = new SelectList(ListDistrito(), "IDE_DIS", "NOM_DIS", obj.IDE_DIS);
                return View(obj);
            }
         
 return View();
        }
        [HttpPost]
        public ActionResult actualizarDoctor(DOCTOR obj)
        {
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("http://localhost/ApiCL3/");
            var request = clienteHttp.PutAsync("api/Doctor", obj, new
            JsonMediaTypeFormatter()).Result;
            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<int>(resultString);
                if (estado == 1)
                {
                    ViewBag.especialidad = new SelectList(ListEspecialidad(), "IDE_ESP", "DES_ESP", obj.IDE_ESP);
                    ViewBag.distrito = new SelectList(ListDistrito(), "IDE_DIS", "NOM_DIS", obj.IDE_DIS);
                    ViewBag.mensaje = estado + " Empleado actualizado correctamente..!!";
                    return View(obj);
                }
                ViewBag.especialidad = new SelectList(ListEspecialidad(), "IDE_ESP", "DES_ESP", obj.IDE_ESP);
                ViewBag.distrito = new SelectList(ListDistrito(), "IDE_DIS", "NOM_DIS", obj.IDE_DIS);
                return View(obj);
            }
            return View();
        }
        [HttpGet]
        public ActionResult eliminarDoctor(int id)
        {
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("http://localhost/ApiCL3/");
            var request = clienteHttp.DeleteAsync("Api/Doctor?id=" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                var estado = JsonConvert.DeserializeObject<bool>(resultString);
                if (estado)
                {
                    return RedirectToAction("ListadoDoctores");
                }
            }
            return View();
        }

    }
}