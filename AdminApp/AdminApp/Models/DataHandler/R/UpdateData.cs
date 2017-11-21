using RserveCLI2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Models.DataHandler.R
{
    public class UpdateData
    {
        public static void UpdateDataFunc()
        {
            try
            {
                RConnection connection = RConnection.Connect(addr: new IPAddress(new byte[] { 11, 23, 58, 53 }), port: 6312, credentials: new NetworkCredential("train", "Zaq@12345"));
                connection.VoidEval("source('C:/Users/Administrator/Documents/RScript/ConnectSQL_SaveNewRtree.R')");
                connection.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
