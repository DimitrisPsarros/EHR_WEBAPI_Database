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
    public class CommunicationsController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/Communications
        public IQueryable<Communication> GetCommunications()
        {
            return db.Communications;
        }

        // GET: api/Communications/5
        [ResponseType(typeof(CommunicationsDetails))]
        public async Task<IHttpActionResult> GetCommunication(int id)
        {
            /*
            Communication communication = await db.Communications.FindAsync(id);
            if (communication == null)
            {
                return NotFound();
            }

            return Ok(communication);
            */
            ////////////////////////////////////
            var communicat = await db.Communications.Where(x => x.PersonID == id).ToListAsync();
            if (communicat == null)
            {
                return NotFound();
            }

            CommunicationsDetails cl = new CommunicationsDetails();

            for (int i = 0; i < communicat.Count; i++)
            {
                cl.email = communicat[i].email;
                cl.Telephone = communicat[i].Telephone;
            }

            return Ok(cl);



        }

        // PUT: api/Communications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCommunication(int id, Communication communication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != communication.CommunicationID)
            {
                return BadRequest();
            }

            db.Entry(communication).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunicationExists(id))
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

        // POST: api/Communications
        [ResponseType(typeof(Communication))]
        public async Task<IHttpActionResult> PostCommunication(Communication communication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Communications.Add(communication);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = communication.CommunicationID }, communication);
        }

        // DELETE: api/Communications/5
        [ResponseType(typeof(Communication))]
        public async Task<IHttpActionResult> DeleteCommunication(int id)
        {
            Communication communication = await db.Communications.FindAsync(id);
            if (communication == null)
            {
                return NotFound();
            }

            db.Communications.Remove(communication);
            await db.SaveChangesAsync();

            return Ok(communication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommunicationExists(int id)
        {
            return db.Communications.Count(e => e.CommunicationID == id) > 0;
        }
    }
}