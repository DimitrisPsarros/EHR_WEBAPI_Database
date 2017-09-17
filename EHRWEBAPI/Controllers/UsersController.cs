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
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace EHRWEBAPI.Controllers
{
    public class UsersController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/Users
        public IQueryable<UserDetails> GetUsers()
        {
            // return db.Users;
            /////////////////// new code
            var UserInfo = from b in db.Users
                           select new UserDetails()        
                           {
                               PersonID = b.PersonID,
                               IsDoctor = b.IsDoctor
                           };
            return UserInfo;

            ////////////////// end new code
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        

        // POST: api/Users
        [ResponseType(typeof(UserDetails))]
        public async Task<IHttpActionResult> /*Task<UserDetails>*/ PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var contentType = Request.Content.Headers.ContentType.MediaType;
            var requestParams = Request.Content.ReadAsStringAsync().Result;
            if (contentType == "application/json")
            {
                List<SaltDetails> listSalt = new List<SaltDetails>();
                
                var userInfo = db.Users.FirstOrDefault(c => c.UserName == user.UserName);

                if (userInfo == null)
                {
                    return NotFound();
                }
                var saltFromDatabase = userInfo.Salt;
                
                var Inputhash = user.Password + saltFromDatabase ;
                string saltedpassword = SHA1(SHA1(Inputhash));
            
                var userInfo1 = db.Users.FirstOrDefault(c => (c.UserName == user.UserName) &&  (c.Password == saltedpassword) );
                if (userInfo1 == null)
                {
                    return NotFound();
                }
                UserDetails userDetails = new UserDetails();
                userDetails.PersonID =userInfo1.PersonID;
                userDetails.IsDoctor= userInfo1.IsDoctor;
                return Ok(userDetails);
            }
            else
            {
                return Ok(HttpStatusCode.UnsupportedMediaType);  
            }
        }


        public string SHA1(string Salt)
        {
            byte[] hash;
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(Salt));
            }
            var sb = new StringBuilder();
            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }
        
        /*
        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
               // return BadRequest(ModelState);
                return null;
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
        }         
        */
        
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}