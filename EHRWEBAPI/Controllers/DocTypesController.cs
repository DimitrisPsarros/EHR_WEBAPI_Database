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
    public class DocTypesController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/DocTypes
        public IQueryable<DocType> GetDocTypes()
        {
            return db.DocTypes;
        }

        // GET: api/DocTypes/5
        [ResponseType(typeof(DocType))]
        public async Task<IHttpActionResult> GetDocType(int id)
        {
            DocType docType = await db.DocTypes.FindAsync(id);
            if (docType == null)
            {
                return NotFound();
            }

            return Ok(docType);
        }

        // PUT: api/DocTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDocType(int id, DocType docType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != docType.Document_typeID)
            {
                return BadRequest();
            }

            db.Entry(docType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocTypeExists(id))
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

        // POST: api/DocTypes
        [ResponseType(typeof(DocType))]
        public async Task<IHttpActionResult> PostDocType(DocType docType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DocTypes.Add(docType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = docType.Document_typeID }, docType);
        }

        // DELETE: api/DocTypes/5
        [ResponseType(typeof(DocType))]
        public async Task<IHttpActionResult> DeleteDocType(int id)
        {
            DocType docType = await db.DocTypes.FindAsync(id);
            if (docType == null)
            {
                return NotFound();
            }

            db.DocTypes.Remove(docType);
            await db.SaveChangesAsync();

            return Ok(docType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DocTypeExists(int id)
        {
            return db.DocTypes.Count(e => e.Document_typeID == id) > 0;
        }
    }
}