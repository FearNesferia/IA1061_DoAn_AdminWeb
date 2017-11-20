using AdminApp.Models;
using AdminApp.Models.DataHandler;
using IISServerModules.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace AdminApp.Controllers.ApiControllers
{
    public class TrafficPakagesController : ApiController
    {
        private PakageDBContext context = new PakageDBContext();
        [HttpPost]
        [Route("v1/api/TrafficPakages/")]
        public IHttpActionResult Post([FromBody]HttpRequest request)
        {
            string path = request.Path;
            string queryString = request.Url.Query;
            string payload;
            using (Stream receiveStream = request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    payload = readStream.ReadToEnd();
                }
            }
            TrafficPackage package = new TrafficPackage(path, queryString, payload);
            TrafficPackage tp = this.context.TrafficPackages.FirstOrDefault(x => x.Id == package.Id);

            if (tp != null)
            {
                return Json(new { isAttack = tp.IsAttack });
            }
            this.context.TrafficPackages.Add(package);
            // Call R here

            return Json(new { isAttack = package.IsAttack });
        }
    }
}