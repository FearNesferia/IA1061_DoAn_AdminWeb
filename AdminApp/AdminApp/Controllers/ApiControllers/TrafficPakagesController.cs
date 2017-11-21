using AdminApp.Models;
using AdminApp.Models.DataHandler;
using AdminApp.Models.DataHandler.R;
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

        [HttpGet]
        [Route("v1/api/TrafficPakages")]
        public IHttpActionResult Get()
        {
            return Json(new { test = true });
        }

        [HttpPost]
        [Route("v1/api/TrafficPakages")]
        public IHttpActionResult Post([FromBody]TrafficPackageModel request)
        {

            TrafficPackage package = new TrafficPackage(request.Path, request.QueryString, request.Payload);
            //TrafficPackage tp = this.context.TrafficPackages.FirstOrDefault(x => x.Id == package.Id);

            //if (tp != null)
            //{
            //    return Json(new { isAttack = tp.IsAttack });
            //}
            //this.context.TrafficPackages.Add(package);

            // Call R here
            AnalyzePacket rHandler = new AnalyzePacket();
            package.IsAttack = rHandler.GetAnalyzePacketResult(new int[] { package.LengthOfArguments, package.NumberOfArguments, package.NumberOfDigitsInArguments, package.NumberOfOtherCharInArguments, package.NumberOfDigitsInPath, package.NumberOfSpecialCharInArguments, package.LengthOfPath, package.LengthOfRequest, package.NumberOfLettersInArguments, package.NumberOfLettersCharInPath, package.NumberOfSepicalCharInPath, 0 });
            rHandler.DisposeConnection();

            return Json(new { isAttack = package.IsAttack });
        }
    }
}