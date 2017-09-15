using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EHRWEBAPI.Models;

namespace EHRWEBAPI.Controllers
{
    public class Treat_MedicinesController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/Treat_Medicines
        public IQueryable<Treat_Medicines> GetTreat_Medicines()
        {
            return db.Treat_Medicines;
        }

        // GET: api/Treat_Medicines/5
        [ResponseType(typeof(Treat_Medicines))]
        public async Task<IHttpActionResult> GetTreat_Medicines(int id)
        {
            Treat_Medicines treat_Medicines = await db.Treat_Medicines.FindAsync(id);
            if (treat_Medicines == null)
            {
                return NotFound();
            }
            return Ok(treat_Medicines);
        }

        [Route("api/Treat_Medicine/{Visitid}")]
        [HttpGet]
        [ResponseType(typeof(Treat_Medicines))]
        public IQueryable<Treat_Medicines> GetMed(int Visitid)
        {
            return db.Treat_Medicines.Where(c => c.VisitID == Visitid);
        }

        // PUT: api/Treat_Medicines/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTreat_Medicines(int id, Treat_Medicines treat_Medicines)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treat_Medicines.TreatmentID)
            {
                return BadRequest();
            }

            db.Entry(treat_Medicines).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Treat_MedicinesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Treat_Medicines
        [ResponseType(typeof(Treat_Medicines))]
        public async Task<IHttpActionResult> PostTreat_Medicines(Treat_Medicines treat_Medicines)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treat_Medicines.Add(treat_Medicines);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = treat_Medicines.TreatmentID }, treat_Medicines);
        }

        // DELETE: api/Treat_Medicines/5
        [ResponseType(typeof(Treat_Medicines))]
        public async Task<IHttpActionResult> DeleteTreat_Medicines(int id)
        {
            Treat_Medicines treat_Medicines = await db.Treat_Medicines.FindAsync(id);
            if (treat_Medicines == null)
            {
                return NotFound();
            }

            db.Treat_Medicines.Remove(treat_Medicines);
            await db.SaveChangesAsync();

            return Ok(treat_Medicines);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Treat_MedicinesExists(int id)
        {
            return db.Treat_Medicines.Count(e => e.TreatmentID == id) > 0;
        }
    }
}