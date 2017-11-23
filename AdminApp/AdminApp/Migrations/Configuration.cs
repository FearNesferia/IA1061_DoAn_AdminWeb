namespace AdminApp.Migrations
{
    using AdminApp.Models.DataHandler;
    using IISServerModules.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal sealed class Configuration : DbMigrationsConfiguration<AdminApp.Models.DataHandler.PakageDBContext>
    {
        private PakageDBContext context = new PakageDBContext();
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AdminApp.Models.DataHandler.PakageDBContext context)
        {

        }

        private void InsertFromTxt(string txtPath)
        {
            string specialChar = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

            string txt = System.IO.File.ReadAllText(txtPath);
            int isAttack = txtPath.Contains("anomalous") ? 1 : 0;
            List<string> splitTxt = Regex.Split(txt, @"(?=POST|GET)").ToList();
            splitTxt.RemoveAll(x => string.IsNullOrEmpty(x));
            foreach (string item in splitTxt)
            {
                Trace.WriteLine($"Insert {item}");
                string[] s = item.Split('\n');
                s = s.Where(x => !string.IsNullOrEmpty(x) && x != "\r").ToArray();

                TrafficPackage request = new TrafficPackage();

                string[] temp = s[0].Split(' ', '?');
                string argument = item.StartsWith("GET") ? (s[0].Contains('?') ? temp[2] : "") : s.Last();
                string path = temp[1].Substring(temp[1].IndexOf("8080") + 4);

                request.LengthOfPath = path.Length;
                request.LengthOfRequest = (item.StartsWith("GET")) ? 0 : int.Parse((s.FirstOrDefault(x => x.Contains("Content-Length: "))).Substring(("Content-Length: ").Length));
                request.LengthOfArguments = argument.Length;
                request.NumberOfDigitsInPath = path.Count(x => char.IsDigit(x));
                request.NumberOfSepicalCharInPath = path.Count(x => specialChar.Contains(x));
                request.NumberOfLettersCharInPath = path.Count(x => char.IsLetter(x));
                request.NumberOfArguments = (argument.Length != 0) ? argument.Split('&').Length : 0;
                request.NumberOfDigitsInArguments = argument.Count(x => char.IsDigit(x));
                request.NumberOfSpecialCharInArguments = argument.Count(x => specialChar.Contains(x));
                request.NumberOfLettersInArguments = argument.Count(x => char.IsLetter(x));
                request.NumberOfOtherCharInArguments = request.LengthOfArguments - (request.NumberOfDigitsInArguments + request.NumberOfLettersInArguments + request.NumberOfSpecialCharInArguments);
                request.TrafficPackageId = Math.Abs((path + argument).GetHashCode());
                request.IsChecked = true;
                context.TrafficPackages.AddOrUpdate(x => x.TrafficPackageId,request);
                context.SaveChanges();
            }
            
        }
    }
}
