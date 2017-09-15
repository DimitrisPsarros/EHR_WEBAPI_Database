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
    public class DoctorPatientRelationsController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/DoctorPatientRelations
        public IQueryable<DoctorPatientRelation> GetDoctorPatientRelations()
        {
            return db.DoctorPatientRelations;
        }

        [Route("api/DoctorPatientFriends/{Personid}")]
        [HttpGet]
        [ResponseType(typeof(ContactId))]
        public IQueryable<ContactId> GetFriends0(int Personid)
        {
            var friends = from b in db.DoctorPatientRelations.
                            Where(c => (c.PatientID == Personid) || (c.DoctorID == Personid))
                          select new ContactId()
                          {
                              contact1 = b.DoctorID,
                              contact2 = b.PatientID
                          };
            return friends;
        }

        [Route("api/DoctorPatientFriendShip/{Personid}")]
        [HttpGet]
        [ResponseType(typeof(DoctorPatientRelation))]
        public IQueryable<DoctorPatientRelation> GetFriends1(int Personid)
        {
            return db.DoctorPatientRelations.Where( c=> (c.PatientID == Personid)|| (c.DoctorID == Personid) );
        }

        [Route("api/PatientFriends/{Personid}")]
        [HttpGet]
        [ResponseType(typeof(friends))]
        public IQueryable<friends> GetFriends2(int Personid)
        {
            var Friends = from b in db.DoctorPatientRelations.
                            Where(c => c.PatientID == Personid )
                            select new friends()
                            {
                                PersonID = b.Doctor.DoctorID,
                                FirstName = b.Doctor.FirstName,
                                LastName =b.Doctor.LastName
                            };
            return Friends;
        }

        [Route("api/DoctorFriends/{Personid}")]
        [HttpGet]
        [ResponseType(typeof(friends))]
        public IQueryable<friends> GetFriends3(int Personid)
        {
            var Friends = from b in db.DoctorPatientRelations.
                            Where(c => c.DoctorID == Personid)
                            select new friends()
                            {
                                PersonID = b.Patient.PatientID,
                                FirstName = b.Patient.FirstName,
                                LastName = b.Patient.LastName
                            };
            return Friends;
        }


        // GET: api/DoctorPatientRelations/5
        [ResponseType(typeof(DoctorPatientRelation))]
        public async Task<IHttpActionResult> GetDoctorPatientRelation(int id)
        {
            DoctorPatientRelation doctorPatientRelation = await db.DoctorPatientRelations.FindAsync(id);
            if (doctorPatientRelation == null)
            {
                return NotFound();
            }

            return Ok(doctorPatientRelation);
        }

        // PUT: api/DoctorPatientRelations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDoctorPatientRelation(int id, DoctorPatientRelation doctorPatientRelation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctorPatientRelation.RelationID)
            {
                return BadRequest();
            }

            db.Entry(doctorPatientRelation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorPatientRelationExists(id))
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

        // POST: api/DoctorPatientRelations
        [ResponseType(typeof(DoctorPatientRelation))]
        public async Task<IHttpActionResult> PostDoctorPatientRelation(DoctorPatientRelation doctorPatientRelation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DoctorPatientRelations.Add(doctorPatientRelation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = doctorPatientRelation.RelationID }, doctorPatientRelation);
        }

        // DELETE: api/DoctorPatientRelations/5
        [ResponseType(typeof(DoctorPatientRelation))]
        public async Task<IHttpActionResult> DeleteDoctorPatientRelation(int id)
        {
            DoctorPatientRelation doctorPatientRelation = await db.DoctorPatientRelations.FindAsync(id);
            if (doctorPatientRelation == null)
            {
                return NotFound();
            }

            db.DoctorPatientRelations.Remove(doctorPatientRelation);
            await db.SaveChangesAsync();

            return Ok(doctorPatientRelation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorPatientRelationExists(int id)
        {
            return db.DoctorPatientRelations.Count(e => e.RelationID == id) > 0;
        }
    }
}