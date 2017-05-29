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
using System.IO;

namespace EHRWEBAPI.Controllers
{
    public class DataSendersController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        // GET: api/DataSenders
        public IQueryable<PhotoData> GetDataSenders()
        {
            //   return db.DataSenders;


            var photodata = from b in db.DataSenders
                            select new PhotoData()
                            {
                               // photobyte = b.PictureInfo          // check this it was changed
                            };
            return photodata;

        }

        // GET: api/DataSenders/5
        [ResponseType(typeof(DataSender))]
        public async Task<IHttpActionResult> GetDataSender(int id)
        {
            DataSender dataSender = await db.DataSenders.FindAsync(id);
            if (dataSender == null)
            {
                return NotFound();
            }

            return Ok(dataSender);
        }

        // PUT: api/DataSenders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDataSender(int id, DataSender dataSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dataSender.DataSenderID)
            {
                return BadRequest();
            }

            db.Entry(dataSender).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSenderExists(id))
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






        /// //////////////////////////////new



        [Route("api/Dat")]
        [HttpPost]
        [ResponseType(typeof(DataSender))]
        // public string

        public async Task<IHttpActionResult> UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var provider1 = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider1);

            await Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((tsk) =>
            {
                // MultipartMemoryStreamProvider prvdr = tsk.Result;
                // Stream InputStream;
                int i = 1;
                foreach (HttpContent ctnt in provider1.Contents)
                {

                    // You would get hold of the inner memory stream here

                    Stream stream = ctnt.ReadAsStreamAsync().Result;

                    if (i == 1)
                    {
                        Stream InputStream = stream;
                        byte[] result;
                        using (var streamReader = new MemoryStream())
                        {
                            InputStream.CopyTo(streamReader);
                            result = streamReader.ToArray();
                            DataSender datasender1 = new DataSender();

                           
                            datasender1.Picture = result;

                            db.DataSenders.Add(datasender1);

                            /*
                             string strHex = BitConverter.ToString(result);
                             Guid id = new Guid(strHex)
                             byte[] id = BitConverter.ToByte(result);
                             Guid guid2 = new Guid(strHex);                              */

                           // db.DataSenders.Add(datasender1);

                            // await db.SaveChangesAsync();
                            //  return CreatedAtRoute("DefaultApi", new { id = dIagnosis.DiagnosisID }, dIagnosis);
                        }
                        i = 0;
                           
                    }
                }
            });

            await db.SaveChangesAsync();


            //  return CreatedAtRoute("DefaultApi", new { id = dIagnosis.DiagnosisID }, dIagnosis);

            return Ok();
        }





        //////////////////////////////////////   end   new 




/*

        // POST: api/DataSenders
        [ResponseType(typeof(DataSender))]
        public async Task<IHttpActionResult> PostDataSender(DataSender dataSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DataSenders.Add(dataSender);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dataSender.DataSenderID }, dataSender);
        }

        */

        // DELETE: api/DataSenders/5
        [ResponseType(typeof(DataSender))]
        public async Task<IHttpActionResult> DeleteDataSender(int id)
        {
            DataSender dataSender = await db.DataSenders.FindAsync(id);
            if (dataSender == null)
            {
                return NotFound();
            }

            db.DataSenders.Remove(dataSender);
            await db.SaveChangesAsync();

            return Ok(dataSender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DataSenderExists(int id)
        {
            return db.DataSenders.Count(e => e.DataSenderID == id) > 0;
        }
    }
}