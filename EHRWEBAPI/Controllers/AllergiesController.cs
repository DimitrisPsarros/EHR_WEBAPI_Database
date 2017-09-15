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
    public class AllergiesController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/Allergies
        public IQueryable<Allergy> GetAllergies()
        {
            return db.Allergies;
        }

        // GET: api/Allergies/5
        [ResponseType(typeof(Allergy))]
        public async Task<IHttpActionResult> GetAllergy(int id)
        {
            Allergy allergy = await db.Allergies.FindAsync(id);
            if (allergy == null)
            {
                return NotFound();
            }
            return Ok(allergy);
        }

        [Route("api/AllergiesList/{Personid}")]
        [HttpGet]
        [ResponseType(typeof(Allergy))]
        public IQueryable<Allergy> GetAllergies(int PersonId)
        {
            return db.Allergies.Where( c => c.PatientID==PersonId );
        }

        // PUT: api/Allergies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAllergy(int id, Allergy allergy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != allergy.AllergyID)
            {
                return BadRequest();
            }

            db.Entry(allergy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllergyExists(id))
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

        // POST: api/Allergies
        [ResponseType(typeof(Allergy))]
        public async Task<IHttpActionResult> PostAllergy(Allergy allergy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Allergies.Add(allergy);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = allergy.AllergyID }, allergy);
        }

        // DELETE: api/Allergies/5
        [ResponseType(typeof(Allergy))]
        public async Task<IHttpActionResult> DeleteAllergy(int id)
        {
            Allergy allergy = await db.Allergies.FindAsync(id);
            if (allergy == null)
            {
                return NotFound();
            }

            db.Allergies.Remove(allergy);
            await db.SaveChangesAsync();

            return Ok(allergy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AllergyExists(int id)
        {
            return db.Allergies.Count(e => e.AllergyID == id) > 0;
        }
    }
}