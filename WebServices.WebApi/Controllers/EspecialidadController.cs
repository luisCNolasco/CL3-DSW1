using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Data.Model;

namespace WebServices.WebApi.Controllers
{
    public class EspecialidadController : ApiController
    {
        CLINICAEntities db = new CLINICAEntities();

        public IEnumerable<SP_LISTAR_ESPECIALIDAD_Result> Get()
        {
            return db.SP_LISTAR_ESPECIALIDAD();
        }
    }
}
