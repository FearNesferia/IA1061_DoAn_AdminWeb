using RserveCLI2;
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
                connection = RConnection.Connect(addr: new IPAddress(new byte[] { 11, 23, 58, 53 }), port: 6311, credentials: new NetworkCredential("analyze", "zaQ@12345"));
            }
            catch (Exception)
            {
                connection = null;
            }
        }

        public bool GetAnalyzePacketResult(int[] packetInfo)
        {
            //packetInfo example: int[] args_r = new int[] { 320, 13, 58, 0, 1, 48, 28, 0, 214, 23, 4 };
            //11 features: "LengthOfArguments","NumberOfArguments","NumberOfDigitsInArguments","NumberOfOtherCharInArguments","NumberOfDigitsInPath","NumberOfSpecialCharInArguments","LengthOfPath","LengthOfRequest","NumberOfLettersInArguments","NumberOfLettersCharInPath","NumberOfSepicalCharInPath"
            if (connection != null && packetInfo.Length == 12)
            {
                connection.Assign("packet", Sexp.Make(packetInfo));
                Sexp a = connection.Eval("source('C:/Users/Administrator/Documents/RScript/PredictPacket.R')");
                return (int)a[0] == 0 ? false : true;
            }
            return false;
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