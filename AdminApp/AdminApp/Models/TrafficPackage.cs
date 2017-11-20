using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISServerModules.Models
{
    public class TrafficPackage
    {

        #region properties

        //primary key
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        //properties for call primary key and show on web
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Payload { get; set; }
        [Required]
        public bool IsChecked { get; set; }


        //properties for training 
        [Required]
        public bool IsAttack { get; set; }
        [Required]
        public int LengthOfArguments { get; set; }
        [Required]
        public int NumberOfArguments { get; set; }
        [Required]
        public int NumberOfDigitsInArguments { get; set; }
        [Required]
        public int NumberOfOtherCharInArguments { get; set; }
        [Required]
        public int NumberOfDigitsInPath { get; set; }
        [Required]
        public int NumberOfSpecialCharInArguments { get; set; }
        [Required]
        public int LengthOfPath { get; set; }
        [Required]
        public int LengthOfRequest { get; set; }
        [Required]
        public int NumberOfLettersInArguments { get; set; }
        [Required]
        public int NumberOfLettersCharInPath { get; set; }
        [Required]
        public int NumberOfSepicalCharInPath { get; set; }


        #endregion
        /*
         constructor
         Example 1: 
         GET http://localhost:8080/tienda1/publico/anadir.jsp?id=2&nombre=Jam%F3n+Ib%E9rico&precio=85&cantidad=%27%3B+DROP+TABLE+usuarios%3B+SELECT+*+FROM+datos+WHERE+nombre+LIKE+%27%25&B1=A%F1adir+al+carrito HTTP/1.1
         
         => path: /tienda1/publico/anadir.jsp
            arg: id=2&nombre=Jam%F3n+Ib%E9rico&precio=85&cantidad=%27%3B+DROP+TABLE+usuarios%3B+SELECT+*+FROM+datos+WHERE+nombre+LIKE+%27%25&B1=A%F1adir+al+carrito
            payload: 

            unique string = path:{path}arg:{arg}payload:{payload}
            

         Example 2:
         POST http://localhost:8080/tienda1/publico/anadir.jsp HTTP/1.1
         Content-Length: 146
         id=2&nombre=Jam%F3n+Ib%E9rico&precio=85&cantidad=%27%3B+DROP+TABLE+usuarios%3B+SELECT+*+FROM+datos+WHERE+nombre+LIKE+%27%25&B1=A%F1adir+al+carrito
         
         => path: /tienda1/publico/anadir.jsp
            arg: 
            payload: id=2&nombre=Jam%F3n+Ib%E9rico&precio=85&cantidad=%27%3B+DROP+TABLE+usuarios%3B+SELECT+*+FROM+datos+WHERE+nombre+LIKE+%27%25&B1=A%F1adir+al+carrito
         
            calculate hash
         */


        public TrafficPackage()
        {

        }

        public TrafficPackage(string path, string queryString, string payload)
        {
            string specialChar = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            LengthOfPath = path.Length;
            LengthOfRequest = payload.Length;
            LengthOfArguments = queryString.Length;
            NumberOfDigitsInPath = path.Count(x => char.IsDigit(x));
            NumberOfSepicalCharInPath = path.Count(x => specialChar.Contains(x));
            NumberOfLettersCharInPath = path.Count(x => char.IsLetter(x));
            NumberOfArguments = (queryString.Length != 0) ? queryString.Split('&').Length : 0;
            NumberOfDigitsInArguments = queryString.Count(x => char.IsDigit(x));
            NumberOfSpecialCharInArguments = queryString.Count(x => specialChar.Contains(x));
            NumberOfLettersInArguments = queryString.Count(x => char.IsLetter(x));
            NumberOfOtherCharInArguments = LengthOfArguments - (NumberOfDigitsInArguments + NumberOfLettersInArguments + NumberOfSpecialCharInArguments);

            CreatedDate = DateTime.Now;
            Id = Math.Abs((path + queryString + payload).GetHashCode());
            IsChecked = false;
            //HOW TO ASSIGN THIS SHIT 
            //IsAttack = false;
        }

        public TrafficPackage(DateTime createdDate, string path, string queryString, string payload, bool isAttack, int lengthOfArguments, int numberOfArguments, int numberOfDigitsInArguments, int numberOfOtherCharInArguments, int numberOfDigitsInPath, int numberOfSpecialCharInArguments, int lengthOfPath, int lengthOfRequest, int numberOfLettersInArguments, int numberOfLettersCharInPath, int numberOfSepicalCharInPath)
        {
            CreatedDate = createdDate;
            Path = path;
            QueryString = queryString;
            Payload = payload;
            IsAttack = isAttack;
            LengthOfArguments = lengthOfArguments;
            NumberOfArguments = numberOfArguments;
            NumberOfDigitsInArguments = numberOfDigitsInArguments;
            NumberOfOtherCharInArguments = numberOfOtherCharInArguments;
            NumberOfDigitsInPath = numberOfDigitsInPath;
            NumberOfSpecialCharInArguments = numberOfSpecialCharInArguments;
            LengthOfPath = lengthOfPath;
            LengthOfRequest = lengthOfRequest;
            NumberOfLettersInArguments = numberOfLettersInArguments;
            NumberOfLettersCharInPath = numberOfLettersCharInPath;
            NumberOfSepicalCharInPath = numberOfSepicalCharInPath;
            Id = Math.Abs((path + queryString + payload).GetHashCode());
            IsChecked = false;
        }
    }
}
