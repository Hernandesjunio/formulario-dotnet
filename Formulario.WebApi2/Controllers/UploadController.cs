using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Formulario.WebApi2.Controllers
{
    [RoutePrefix("api/upload")]
    [EnableCors("http://localhost:4200", "*", "*")]
    public class UploadController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> Ping()
        {
            string r = await Task.FromResult("Pong");

            return Ok(r);
        }

        [HttpPost]
        [Route("")]
        [AcceptVerbs("OPTIONS")]
        public async Task<List<string>> PostAsync()
        {
            try
            {
                if (Request.Content.IsMimeMultipartContent())
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/app_data/uploads");

                    MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);

                    await Request.Content.ReadAsMultipartAsync(streamProvider);

                    List<string> messages = new List<string>();
                    foreach (MultipartFileData file in streamProvider.FileData)
                    {
                        FileInfo fi = new FileInfo(file.LocalFileName);
                        messages.Add(fi.Name);
                    }

                    return messages;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                    throw new HttpResponseException(response);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
