using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Data.Model;

namespace WebServices.WebApi.Controllers
{
    public class DoctorController : ApiController
    {
        CLINICAEntities db = new CLINICAEntities();

        public IEnumerable<SP_LISTAR_DOCTOR_Result> Get()
        {
            return db.SP_LISTAR_DOCTOR();
        }
        [HttpGet]
        public DOCTOR Get(int id)
        {
            var unDoctor = db.DOCTOR.Where(p => p.IDE_DOC == id).FirstOrDefault();
            return unDoctor;
        }

        //registrar doctor
        [HttpPost]
        public int Post(DOCTOR objD)
        {
            db.DOCTOR.Add(objD);
            return db.SaveChanges();
        }

        //actualizacion
        [HttpPut]
        public int Put(DOCTOR objD)
        {
            var unDoctor = db.DOCTOR.Where(p => p.IDE_DOC == objD.IDE_DOC).FirstOrDefault();

            unDoctor.IDE_DOC = objD.IDE_DOC;
            unDoctor.NOM_DOC = objD.NOM_DOC;
            unDoctor.APE_DOC = objD.APE_DOC;
            unDoctor.IDE_ESP = objD.IDE_ESP;
            unDoctor.TEL_DOC = objD.TEL_DOC;
            unDoctor.COR_DOC = objD.COR_DOC;
            unDoctor.IDE_DIS = objD.IDE_DIS;

            return db.SaveChanges();
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var unDoctor = db.DOCTOR.Where(p => p.IDE_DOC == id).FirstOrDefault();
            db.DOCTOR.Remove(unDoctor);
            return db.SaveChanges()>0;
        }
    }
}
