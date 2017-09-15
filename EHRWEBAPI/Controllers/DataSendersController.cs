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
using System.Drawing.Imaging;
using System.Reflection;
using System.Web;
using System.Drawing;


namespace EHRWEBAPI.Controllers
{
    public class DataSendersController : ApiController
    {
        private EHRsystemEntities db = new EHRsystemEntities();

        [Route("api/MessagesCommunication/{Personid}/{Contacid}")]
        [HttpGet]
        [ResponseType(typeof(MessagesChat))]
        public IQueryable<MessagesChat> GetMessages(int Personid, int Contacid)         // messages between patient-doctor<<ChatBubble>>
        {
            var photodata = from b in db.DataSenders.
                            Where(c => (((c.PatientID == Personid) && (c.DoctorID == Contacid)) || ((c.PatientID == Contacid) && (c.DoctorID == Personid))) && (c.Picture == null) && (c.PictureInfo == null))
                            select new MessagesChat()
                            {
                                Date = b.Date,
                                Text = b.Text,
                                IsMe = b.Sender    // it is zero when sender is the patient
                            };
            return photodata;
        }

        [Route("api/Messages2/{Personid}/{Contacid}")]
        [HttpGet]
        [ResponseType(typeof(ImagesIDs))]
        public IQueryable<ImagesIDs> GetMessages2(int Personid, int Contacid)
        {
            var photodata = from b in db.DataSenders.
                            Where(c => (((c.PatientID == Personid) && (c.DoctorID == Contacid) && (c.Picture != null)) || ((c.PatientID == Contacid) && (c.DoctorID == Personid) && (c.Picture != null))) )
                            select new ImagesIDs()
                            {
                                DataSenderId = b.DataSenderID
                            };
            return photodata;
        }

        [Route("api/Messages3/{Personid}/{Contacid}")]
        [HttpGet]
        [ResponseType(typeof(ImagesIDs))]
        public IQueryable<ImagesIDs> GetMessages29(int Personid, int Contacid)
        {
            var photodata = from b in db.DataSenders.
                            Where(c => (((c.PatientID == Personid) && (c.DoctorID == Contacid) && (c.Picture != null)) ))
                            select new ImagesIDs()
                            {
                                DataSenderId = b.DataSenderID,
                                Date         = b.Date,
                                FirstName    = b.Doctor.FirstName,
                                LastName     = b.Doctor.LastName,
                                FirstName1   = b.Patient.FirstName,
                                LastName1    = b.Patient.LastName,
                                Text         = b.Text,
                                Sender       = b.Sender
                            };
            return photodata;
        }

        [Route("api/Messages4/{Personid}/{Contacid}")]
        [HttpGet]
        [ResponseType(typeof(ImagesIDs))]
        public IQueryable<ImagesIDs> GetMessages30(int Personid, int Contacid)
        {
            var photodata = from b in db.DataSenders.
                            Where(c => ( (c.PatientID == Contacid) && (c.DoctorID == Personid) && (c.Picture != null) ))
                            select new ImagesIDs()
                            {
                                DataSenderId = b.DataSenderID

                            };
            return photodata;
        }

        [Route("api/Messages23/{DatasenderId}")]
        [HttpGet]
        [ResponseType(typeof(ImagesChat))]
        public IQueryable<ImagesChat> GetMessages23(int DatasenderId)
        {
            var photodata1 = from b in db.DataSenders.
                            Where(c => c.DataSenderID == DatasenderId)
                            select new ImagesChat()
                            {
                                Picture = b.Picture,
                                //Date = b.Date                                                // allaxthke
                            };
            
            return photodata1;
        }
        
        
        ////////////////////  Patient New Messages-Photos  //////////////////////////
        

        [Route("api/PatientNewMessages/{PatientId}")]          // Not seen New messages of Patient <<DemographicActivity>>
        [HttpGet]
        [ResponseType(typeof(New_Messages))]
        public IQueryable<New_Messages> GetMessages24(int PatientId)
        {
                             var photodata1 = from b in db.DataSenders.
                             Where(c => ( (c.PatientID == PatientId) && (c.Sender == 1) && (c.Picture == null ) && (c.Seen == false) ))
                             select new New_Messages()
                             {
                                 DataSenderID = b.DataSenderID,
                                 PersonID     = b.DoctorID, 
                                 FirstName    = b.Doctor.FirstName,
                                 LastName     = b.Doctor.LastName,
                                 Text         = b.Text,
                                 Date         = b.Date,
                                 Seen         = b.Seen
                             };
            return photodata1;
        }

        [Route("api/PatientNewMessages1/{PatientId}")]          // Not received New messages of Patient <<LoginActivity>>
        [HttpGet]
        [ResponseType(typeof(New_Messages))]
        public IQueryable<New_Messages> GetMessages27(int PatientId)
        {
                             var photodata = from b in db.DataSenders.
                             Where(c => ((c.PatientID == PatientId) && (c.Sender == 1) && (c.Picture == null) && c.Send == false))
                             select new New_Messages()
                             {
                                 DataSenderID = b.DataSenderID,
                                 PersonID     = b.DoctorID,
                                 FirstName    = b.Doctor.FirstName,
                                 LastName     = b.Doctor.LastName,
                                 Text         = b.Text,
                                 Date         = b.Date,
                             };
            return photodata;
        }

        [Route("api/PatientNewImages1/{PatientId}")]          // Not received New Images of Patient <<LoginActivity>>
        [HttpGet]
        [ResponseType(typeof(New_Messages))]
        public IQueryable<New_Messages> GetMessages35(int PatientId)
        {
            var photodata = from b in db.DataSenders.
            Where(c => ((c.PatientID == PatientId) && (c.Sender == 1) && (c.Picture != null) && c.Send == false))
                            select new New_Messages()
                            {
                                DataSenderID = b.DataSenderID,
                                PersonID     = b.DoctorID,
                                FirstName    = b.Doctor.FirstName,
                                LastName     = b.Doctor.LastName,
                                Text         = b.Text,
                                Date         = b.Date,
                            };
            return photodata;
        }

        [Route("api/PatientNewPhotos/{PatientId}")]
        [HttpGet]
        [ResponseType(typeof(New_Messages))]
        public IQueryable<New_Messages> GetMessages33(int PatientId)
        {
                             var photodata = from b in db.DataSenders.
                             Where(c => ((c.PatientID == PatientId) && (c.Sender == 1) && (c.Picture != null) && c.Seen == false))
                             select new New_Messages()
                             {
                                 DataSenderID = b.DataSenderID,
                                 PersonID     = b.DoctorID,
                                 FirstName    = b.Doctor.FirstName,
                                 LastName     = b.Doctor.LastName,
                                 Text         = b.Text,
                                 Date         = b.Date,
                                 Seen         = b.Seen
                             };
            return photodata;
        }

        [Route("api/PatientNewMessages2/{DatasenderId}")]
        [HttpGet]
        [ResponseType(typeof(DataSender))]
        public IQueryable<DataSender> GetMessages25(int DatasenderId)
        {
            return db.DataSenders.Where( c => ( (c.PatientID == DatasenderId) && (c.Sender == 1) && (c.Picture==null)  && c.Seen == false) );
        }
        
        ////////////////////  Doctor New Messages-Photos  //////////////////////////


        [Route("api/DoctorNewMessages/{DatasenderId}")]   
        [HttpGet]
        [ResponseType(typeof(New_Messages))]
        public IQueryable<New_Messages> GetMessages26(int DatasenderId)
        {
                             var Data = from b in db.DataSenders.
                             Where(c => ((c.DoctorID == DatasenderId) && (c.Sender == 0) && (c.Picture == null) && c.Seen == false))
                             select new New_Messages()
                             {
                                 DataSenderID = b.DataSenderID,
                                 PersonID     = b.PatientID,
                                 FirstName    = b.Patient.FirstName,
                                 LastName     = b.Patient.LastName,
                                 Text         = b.Text,
                                 Date         = b.Date,
                                 Seen         = b.Seen
                             };
            return Data;
        }

        [Route("api/DoctorNewMessages1/{DatasenderId}")]
        [HttpGet]
        [ResponseType(typeof(New_Messages))]
        public IQueryable<New_Messages> GetMessages29(int DatasenderId)
        {
                             var Data = from b in db.DataSenders.
                             Where(c => ((c.DoctorID == DatasenderId) && (c.Sender == 0) && (c.Picture == null) && c.Send == false))
                             select new New_Messages()
                             {
                                DataSenderID = b.DataSenderID,
                                PersonID     = b.PatientID,
                                FirstName    = b.Patient.FirstName,
                                LastName     = b.Patient.LastName,
                                Text         = b.Text,
                                Date         = b.Date
                             };
            return Data;
        }

        [Route("api/DoctorNewImages1/{DatasenderId}")]
        [HttpGet]
        [ResponseType(typeof(New_Messages))]
        public IQueryable<New_Messages> GetMessages39(int DatasenderId)
        {
            var Data = from b in db.DataSenders.
            Where(c => ((c.DoctorID == DatasenderId) && (c.Sender == 0) && (c.Picture != null) && c.Send == false))
                       select new New_Messages()
                       {
                           DataSenderID = b.DataSenderID,
                           PersonID     = b.PatientID,
                           FirstName    = b.Patient.FirstName,
                           LastName     = b.Patient.LastName,
                           Text         = b.Text,
                           Date         = b.Date
                       };
            return Data;
        }

        [Route("api/DoctorNewPhotos/{DatasenderId}")]
        [HttpGet]
        [ResponseType(typeof(New_Messages))]
        public IQueryable<New_Messages> GetMessages32(int DatasenderId)
        {
                             var Data = from b in db.DataSenders.
                             Where(c => ((c.DoctorID == DatasenderId) && (c.Sender == 0) && (c.Picture != null) && c.Seen == false))
                             select new New_Messages()
                             {
                                 DataSenderID = b.DataSenderID,
                                 PersonID     = b.PatientID,
                                 FirstName    = b.Patient.FirstName,
                                 LastName     = b.Patient.LastName,
                                 Text         = b.Text,
                                 Date         = b.Date,
                                 Seen         = b.Seen
                             };
            return Data;
        }


        // GET: api/DataSenders
        public IQueryable<DataSender> GetDataSenders()
        {
               return db.DataSenders;
        }
        
        // GET: api/DataSenders/5
        [ResponseType(typeof(ImagesChat))]                          // get specific image <<NewImagesActivity,ImagesActivity>>
        public async Task<IHttpActionResult> GetDataSender(int id)
        {
                            var Data = from b in db.DataSenders.
                            Where(c => c.DataSenderID == id)
                             select new ImagesChat()
                             {
                                 Picture = b.Picture,
                                 Date    = b.Date,
                                 Text    = b.PictureInfo
                             };
            return Ok(Data);
        }
        

        // PUT: api/DataSenders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDataSender(int id, DataSender dataSender)
        {
            var data = db.DataSenders.FirstOrDefault(c => c.DataSenderID == dataSender.DataSenderID);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                data.Send = dataSender.Send;
                data.Seen = dataSender.Seen;
            }
            //return Ok();

            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dataSender.DataSenderID)
            {
                return BadRequest();
            }

            db.Entry(dataSender).State = EntityState.Modified;
            */
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
        

        //[Route("api/Data/{id}")]      
        //[HttpGet]
        //[ResponseType(typeof(DataSender))]

        //public async Task<HttpResponseMessage> GetImage(int id)
        //{
        //    DataSender dataSender = await db.DataSenders.FindAsync(id);

        //    byte[] pict = dataSender.Picture;
            
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //    result.Content = new ByteArrayContent(pict.ToArray());
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        //    return result;
        //}
        
        [Route("api/DataSenderImage")]
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
                            StreamReader reader1 = new StreamReader(InputStream1);
                            string text1 = reader1.ReadToEnd();
                            int Text1 = Int32.Parse(text1);         
                            datasender1.PatientID = Text1;                                   
                            break;
                        case 2:
                            Stream InputStream2 = stream;
                            StreamReader reader2 = new StreamReader(InputStream2);
                            string text2 = reader2.ReadToEnd();
                            int Text2 = Int32.Parse(text2);          
                            datasender1.DoctorID = Text2;                                 
                            break;
                        case 3:
                            Stream InputStream3 = stream;
                            StreamReader reader3 = new StreamReader(InputStream3);
                            string text3 = reader3.ReadToEnd();
                            int Text3 = Int32.Parse(text3);
                            datasender1.Sender = Text3;
                            break;
                        case 4:
                            Stream InputStream4 = stream;
                            using (var streamReader = new MemoryStream())
                            {
                             InputStream4.CopyTo(streamReader);
                             result = streamReader.ToArray();
                             datasender1.Picture = result;
                            // db.DataSenders.Add(datasender1);\
                             /*
                             string strHex = BitConverter.ToString(result);
                             Guid id = new Guid(strHex)
                             byte[] id1 = BitConverter.ToByte(result);
                             Guid guid2 = new Guid(strHex);                              
                             */ 
                            }
                            break;
                        case 5:
                            Stream InputStream5 = stream;
                            StreamReader reader5 = new StreamReader(InputStream5);
                            string text5 = reader5.ReadToEnd();
                            if (text5.Length != 0)
                            {
                                datasender1.PictureInfo = text5;      // apo string se text
                            }
                            break;
                        case 6: 
                            datasender1.Seen = false;
                            break;
                        case 7:
                            Stream InputStream7 = stream;
                            StreamReader reader7 = new StreamReader(InputStream7);
                            string text7 = reader7.ReadToEnd();
                            DateTime dt = Convert.ToDateTime(text7);
                            datasender1.Date = dt;
                            break;
                        case 8:
                            datasender1.Send = false;
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