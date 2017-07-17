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
using System.Web.Hosting;
using System.Net.Http.Headers;

namespace EHRWEBAPI.Controllers
{
    public class DataSendersController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();


        [Route("api/Messages1/{Personid}/{Contacid}")]
        [HttpGet]
        [ResponseType(typeof(MessagesChat))]
        public IQueryable<MessagesChat> GetMessages(int Personid, int Contacid)
        {
            var photodata = from b in db.DataSenders.
                            Where( c => ( ( ( c.PersonID == Personid ) && ( c.ReseiverID == Contacid ) ) || ( ( c.PersonID == Contacid ) && ( c.ReseiverID == Personid) ) )  &&  (c.Picture == null) && (c.PictureInfo == null))
                            select new MessagesChat()
                            {
                                PersonID = b.PersonID,
                                ReseiverID = b.ReseiverID,
                                Text = b.Text,
                                IsMe = ( b.PersonID == Personid )
                            };
            return photodata;
         }

        [Route("api/Messages2/{Personid}/{Contacid}")]
        [HttpGet]
        [ResponseType(typeof(ImagesIDs))]
        public IQueryable<ImagesIDs> GetMessages2(int Personid, int Contacid)
        {
            var photodata = from b in db.DataSenders.
                            Where(c => (((c.PersonID == Personid) && (c.ReseiverID == Contacid) && (c.Picture != null)) || ((c.PersonID == Contacid) && (c.ReseiverID == Personid) && (c.Picture != null))) )
                            select new ImagesIDs()
                            {
                                DataSenderId =b.DataSenderID
                            };
            return photodata;
        }

        [Route("api/Messages23/{id}")]
        [HttpGet]
        [ResponseType(typeof(ImagesChat))]
        public IQueryable<ImagesChat> GetMessages23(int DatasenderId)
        {
            var photodata1 = from b in db.DataSenders.
                            Where(c => c.DataSenderID == DatasenderId)
                            select new ImagesChat()
                            {
                                Picture = b.Picture,
                                Date = b.Date
                            };
            return photodata1;
        }

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
        [ResponseType(typeof(ImagesChat))]
        public async Task<IHttpActionResult> GetDataSender(int id)
        {
            //DataSender dataSender = await db.DataSenders.FindAsync(id);
            //if (dataSender == null)
            //{
            //    return NotFound();
            //}

            //return Ok(dataSender);

            var photodata1 = from b in db.DataSenders.
                            Where(c => c.DataSenderID == id)
                             select new ImagesChat()
                             {
                                 Picture = b.Picture,
                                 Date = b.Date
                             };
            return Ok(photodata1);
        }

        

            /*
        // GET: api/DataSenders/5
        [ResponseType(typeof(DataSender))]
        public async Task<HttpResponseMessage> GetDataSender(int id)
        {
            DataSender dataSender = await db.DataSenders.FindAsync(id);
            if (dataSender == null)
            {
                return null;
            }
            byte[] pict = dataSender.Picture;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(pict.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return result;

        }

        */





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

        

        //////////////////////////////////////////// new code

        [Route("api/Data/{id}")]                   // den tha pareis apo edw thn photo. tha thn pareis apo ton DataSenders
        [HttpGet]
        [ResponseType(typeof(DataSender))]

        public async Task<HttpResponseMessage> GetImage(int id)
        {
            DataSender dataSender = await db.DataSenders.FindAsync(id);

            byte[] pict = dataSender.Picture;
            
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(pict.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return result;
            
        }

        //////////////////////////////// end new code
       

        [Route("api/Dat")]
        [HttpPost]
        [ResponseType(typeof(DataSender))]

        public async Task<IHttpActionResult> UploadFile()
        {
            byte[] result;
            
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var provider1 = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider1);

            DataSender datasender1 = new DataSender() ;

            await Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((tsk) =>
            {
                // MultipartMemoryStreamProvider prvdr = tsk.Result;
                // Stream InputStream;
                
                int value = 1;

                foreach (HttpContent ctnt in provider1.Contents)
                {
                    
                    Stream stream = ctnt.ReadAsStreamAsync().Result;
                    
                    switch (value)
                    {
                        case 1:

                        Stream InputStream1 = stream;
                        using (var streamReader = new MemoryStream())
                        {
                             InputStream1.CopyTo(streamReader);
                             result = streamReader.ToArray();
                          
                             datasender1.Picture = result;

                            // db.DataSenders.Add(datasender1);
                            
                             /*
                             string strHex = BitConverter.ToString(result);
                             Guid id = new Guid(strHex)
                             byte[] id1 = BitConverter.ToByte(result);
                             Guid guid2 = new Guid(strHex);                              
                             */ 
                        }
                            break;
                        case 2:

                            Stream InputStream2 = stream;
                            StreamReader reader2 = new StreamReader(InputStream2);
                            string text2 = reader2.ReadToEnd();
                            datasender1.PictureInfo = text2;      // apo string se text

                            break;
                        case 3: 

                            Stream InputStream3 = stream;
                            StreamReader reader3 = new StreamReader(InputStream3);
                            string text3 = reader3.ReadToEnd();
                        

                            break;
                        case 4:

                            Stream InputStream4 = stream;
                            StreamReader reader4 = new StreamReader(InputStream4);
                            string text4 = reader4.ReadToEnd();
                            datasender1.Date = text4;

                            break; 
                    }
                    value += 1;
                }
            });

            db.DataSenders.Add(datasender1);

            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = datasender1.DataSenderID }, datasender1);

           // return Ok();
        }
        
        

            

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