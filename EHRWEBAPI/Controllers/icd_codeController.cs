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
    public class icd_codeController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/icd_code
        public IQueryable<icd_code> Geticd_code()
        {
            return db.icd_code;
        }

        // GET: api/icd_code/5
        [ResponseType(typeof(icd_code))]
        public async Task<IHttpActionResult> Geticd_code(int id)
        {
            icd_code icd_code = await db.icd_code.FindAsync(id);
            if (icd_code == null)
            {
                return NotFound();
            }

            return Ok(icd_code);
        }

        // PUT: api/icd_code/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puticd_code(int id, icd_code icd_code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != icd_code.col0)
            {
                return BadRequest();
            }

            db.Entry(icd_code).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!icd_codeExists(id))
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

        // POST: api/icd_code
        [ResponseType(typeof(icd_code))]
        public async Task<IHttpActionResult> Posticd_code(icd_code icd_code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.icd_code.Add(icd_code);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = icd_code.col0 }, icd_code);
        }

        // DELETE: api/icd_code/5
        [ResponseType(typeof(icd_code))]
        public async Task<IHttpActionResult> Deleteicd_code(int id)
        {
            icd_code icd_code = await db.icd_code.FindAsync(id);
            if (icd_code == null)
            {
                return NotFound();
            }

            db.icd_code.Remove(icd_code);
            await db.SaveChangesAsync();

            return Ok(icd_code);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool icd_codeExists(int id)
        {
            return db.icd_code.Count(e => e.col0 == id) > 0;
        }
    }
}