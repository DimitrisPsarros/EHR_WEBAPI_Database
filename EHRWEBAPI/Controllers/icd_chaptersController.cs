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
    public class icd_chaptersController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/icd_chapters
        public IQueryable<icd_chapters> Geticd_chapters()
        {
            return db.icd_chapters;
        }

        // GET: api/icd_chapters/5
        [ResponseType(typeof(icd_chapters))]
        public async Task<IHttpActionResult> Geticd_chapters(string id)
        {
            icd_chapters icd_chapters = await db.icd_chapters.FindAsync(id);
            if (icd_chapters == null)
            {
                return NotFound();
            }

            return Ok(icd_chapters);
        }

        // PUT: api/icd_chapters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puticd_chapters(string id, icd_chapters icd_chapters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != icd_chapters.col1)
            {
                return BadRequest();
            }

            db.Entry(icd_chapters).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!icd_chaptersExists(id))
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

        // POST: api/icd_chapters
        [ResponseType(typeof(icd_chapters))]
        public async Task<IHttpActionResult> Posticd_chapters(icd_chapters icd_chapters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.icd_chapters.Add(icd_chapters);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (icd_chaptersExists(icd_chapters.col1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = icd_chapters.col1 }, icd_chapters);
        }

        // DELETE: api/icd_chapters/5
        [ResponseType(typeof(icd_chapters))]
        public async Task<IHttpActionResult> Deleteicd_chapters(string id)
        {
            icd_chapters icd_chapters = await db.icd_chapters.FindAsync(id);
            if (icd_chapters == null)
            {
                return NotFound();
            }

            db.icd_chapters.Remove(icd_chapters);
            await db.SaveChangesAsync();

            return Ok(icd_chapters);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool icd_chaptersExists(string id)
        {
            return db.icd_chapters.Count(e => e.col1 == id) > 0;
        }
    }
}