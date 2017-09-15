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
    public class DemographicsController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/Demographics
        public IQueryable<DemographicsDetails> GetDemographics()
        {
            var DemographicsInfo = from b in db.Patients
                                   select new DemographicsDetails()
                                   {
                                       PERSONID = b.PatientID,
                                       FirstName = b.FirstName,
                                       LastName = b.LastName,
                                       Sex = b.Sex,
                                       Country = b.Country,
                                       City = b.City,
                                       StreetName = b.StreetName,
                                       StreetNumber = b.StreetNumber,
                                       Birthday = b.Birthday
                                   };
            return DemographicsInfo;
        }

        // GET: api/Demographics/5
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> GetDemographic(int id)
        {
            Patient demographics = await db.Patients.FindAsync(id);
            if (demographics == null)
            {
                return NotFound();
            }

            //DemographicsDetails Demo = new DemographicsDetails();
            //Demo.Birthday = demographics.Birthday;
            //Demo.City = demographics.City;
            //Demo.Country = demographics.Country;
            //Demo.FirstName = demographics.FirstName;
            //Demo.LastName = demographics.LastName;
            //Demo.Sex = demographics.Sex;
            //Demo.StreetName = demographics.StreetName;
            //Demo.StreetNumber = demographics.StreetNumber;
            
            //return Ok(Demo);
            return Ok(demographics);
        }

        [Route("api/FullNamePatient/{Personid}")]
        [HttpGet]
        [ResponseType(typeof(FullNames))]
        public IQueryable<FullNames> GetFullName(int Personid)
        {
            var contactsList = from b in db.Patients.
                            Where(c => (c.PatientID == Personid) )
                               select new FullNames()
                               {
                                   FirstName = b.FirstName,
                                   LastName = b.LastName 
                               };
            return contactsList;
        }

        // PUT: api/Demographics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDemographic(int id, Patient demographic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != demographic.PatientID)
            {
                return BadRequest();
            }

            db.Entry(demographic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemographicExists(id))
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

        // POST: api/Demographics
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> PostDemographic(Patient demographic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(demographic);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = demographic.PatientID }, demographic);
        }

        // DELETE: api/Demographics/5
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> DeleteDemographic(int id)
        {
            Patient demographic = await db.Patients.FindAsync(id);
            if (demographic == null)
            {
                return NotFound();
            }

            db.Patients.Remove(demographic);
            await db.SaveChangesAsync();

            return Ok(demographic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DemographicExists(int id)
        {
            return db.Patients.Count(e => e.PatientID == id) > 0;
        }
    }
}