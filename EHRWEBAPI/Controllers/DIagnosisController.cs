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
    public class DIagnosisController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/DIagnosis
        public IQueryable<DIagnosi> GetDIagnosis()
        {
            return db.DIagnosis; 
        }

        // GET: api/DIagnosis/5
        [ResponseType(typeof(DIagnosi))]
        public async Task<IHttpActionResult> GetDIagnosi(int id)
        {
            DIagnosi dIagnosi = await db.DIagnosis.FindAsync(id);
            if (dIagnosi == null)
            {
                return NotFound();
            }

            return Ok(dIagnosi);
        }

        [Route("api/Diagnosi/{Visitid}")]
        [HttpGet]
        [ResponseType(typeof(DIagnosi))]
        public IQueryable<DIagnosi> GetDiagn(int Visitid)
        {
            return db.DIagnosis.Where(c => c.VisitID == Visitid);
        }

        // PUT: api/DIagnosis/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDIagnosi(int id, DIagnosi dIagnosi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dIagnosi.DiagnosisID)
            {
                return BadRequest();
            }

            db.Entry(dIagnosi).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DIagnosiExists(id))
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

        // POST: api/DIagnosis
        [ResponseType(typeof(DIagnosi))]
        public async Task<IHttpActionResult> PostDIagnosi(DIagnosi dIagnosi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DIagnosis.Add(dIagnosi);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dIagnosi.DiagnosisID }, dIagnosi);
        }

        // DELETE: api/DIagnosis/5
        [ResponseType(typeof(DIagnosi))]
        public async Task<IHttpActionResult> DeleteDIagnosi(int id)
        {
            DIagnosi dIagnosi = await db.DIagnosis.FindAsync(id);
            if (dIagnosi == null)
            {
                return NotFound();
            }

            db.DIagnosis.Remove(dIagnosi);
            await db.SaveChangesAsync();

            return Ok(dIagnosi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DIagnosiExists(int id)
        {
            return db.DIagnosis.Count(e => e.DiagnosisID == id) > 0;
        }
    }
}