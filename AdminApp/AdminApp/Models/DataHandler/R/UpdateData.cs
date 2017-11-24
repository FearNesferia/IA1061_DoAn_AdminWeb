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
        public static int UpdateDataFunc(int websiteId)
        {
            try
            {
                RConnection connection = RConnection.Connect("fearnesferia.ddns.net", port: 6312);
                connection.Assign("id_to_check", Sexp.Make(websiteId));
                Sexp result = connection.Eval("source('C:/Users/Administrator/Documents/RScript/AddOrUpdateRTree.R')");
                connection.Dispose();
                return (int)result[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
