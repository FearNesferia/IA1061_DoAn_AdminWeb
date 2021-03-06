﻿using AdminApp.Models;
using AdminApp.Models.DataHandler;
using AdminApp.Models.DataHandler.R;
using IISServerModules.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [Route("v1/api/TrafficPackages")]
        public IHttpActionResult Post([FromBody]TrafficPackageModel request)
        {
            Website web = this.context.Websites.FirstOrDefault(x => x.Url == request.WebsiteUrl);
            if (web == default(Website))
            {
                return BadRequest();
            }
            TrafficPackage package = new TrafficPackage(request.Path, request.QueryString, request.Payload);
            package.WebsiteId = web.WebsiteId;
            package.Website = web;
            TrafficPackage tp = this.context.TrafficPackages.FirstOrDefault(x => x.TrafficPackageId == package.TrafficPackageId);

            if (tp != null)
            {
                return Json(new { isAttack = tp.IsAttack, isDetectMode = web.IsDetecMode });
            }
            EventLog log = new EventLog();
            log.Source = "Application";
            try
            {
                #region forDemoAndCollecting
                //for demo
                //if (request.Path.Contains("BlogCreate.aspx") && request.Payload.Contains("update"))
                //{
                //    package.IsAttack = true;
                //}

                //collecting package
                //package.IsAttack = false;
                //package.IsChecked = true;
                //save change

                #endregion

            }
            catch (Exception ex)
            {
                log.WriteEntry(ex.Message);
            }
           



            #region call_R
            int dbrow = this.context.TrafficPackages.Count(x => x.WebsiteId == web.WebsiteId);
            // Call R here

            AnalyzePacket rHandler = new AnalyzePacket();
            package.IsAttack = rHandler.GetAnalyzePacketResult(new int[] { package.LengthOfArguments, package.NumberOfArguments, package.NumberOfDigitsInArguments, package.NumberOfOtherCharInArguments, package.NumberOfDigitsInPath, package.NumberOfSpecialCharInArguments, package.LengthOfPath, package.LengthOfRequest, package.NumberOfLettersInArguments, package.NumberOfLettersCharInPath, package.NumberOfSepicalCharInPath, 0 }, package.WebsiteId);
            package.IsChecked = false;
            this.context.TrafficPackages.Add(package);
            this.context.SaveChanges();
            rHandler.DisposeConnection();

            //int newRow = UpdateData.UpdateDataFunc(package.WebsiteId);
            //log.Source = "Application";
            //log.WriteEntry($"{dbrow}|{newRow}");

            #endregion
            return Json(new { isAttack = package.IsAttack, isDetectMode = web.IsDetecMode });
        }
    }
}