﻿using RserveCLI2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Models.DataHandler.R
{
    public class AnalyzePacket
    {
        private RConnection connection;

        public AnalyzePacket()
        {
            try
            {
                connection = RConnection.Connect("fearnesferia.ddns.net", port: 6311);
            }
            catch (Exception ex)
            {
                connection = null;
            }
        }

        public bool GetAnalyzePacketResult(int[] packetInfo, int websiteId)
        {
            //packetInfo example: int[] args_r = new int[] { 320, 13, 58, 0, 1, 48, 28, 0, 214, 23, 4 };
            //12 features: "LengthOfArguments","NumberOfArguments","NumberOfDigitsInArguments","NumberOfOtherCharInArguments","NumberOfDigitsInPath","NumberOfSpecialCharInArguments","LengthOfPath","LengthOfRequest","NumberOfLettersInArguments","NumberOfLettersCharInPath","NumberOfSepicalCharInPath",isattack
            if (connection != null && packetInfo.Length == 12)
            {
                connection.Assign("packet", Sexp.Make(packetInfo));
                connection.Assign("website_id", Sexp.Make(websiteId));
                Sexp a = connection.Eval("source('C:/Users/Administrator/Documents/RScript/PredictPacket.R')");
                return (int)a[0] == 0 ? false : true;
            }
            return true;
        }

        public void DisposeConnection()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }
    }
}