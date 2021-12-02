using RestSharp;
using System;

namespace RestClientProject
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new RestClient("https://localhost:5001/");


            var getSmsRequest = new RestRequest("api/Accounts/GetSmsCode",Method.GET);
            getSmsRequest.AddParameter("PhoneNumber", "09111111111");

            var getsmsResult = client.Get(getSmsRequest);

            Console.WriteLine("Hello World!");
        }
    }
}
