using System;
using System.Configuration;

namespace SfQuery
{
    public class Program
    {
        private static SalesforceClient CreateClient()
        {
            var myEnvironment = ConfigurationManager.AppSettings["myEnvironment"];
            var client = new SalesforceClient();

            if (myEnvironment == "Dev")
            {
                client.Username = ConfigurationManager.AppSettings["username"];
                client.Password = ConfigurationManager.AppSettings["password"];
                client.Token = ConfigurationManager.AppSettings["token"];
                client.ClientId = ConfigurationManager.AppSettings["clientId"];
                client.ClientSecret = ConfigurationManager.AppSettings["clientSecret"];            
            }
            else if (myEnvironment == "Prod")
            {
                client.Username = ConfigurationManager.AppSettings["devUsername"];
                client.Password = ConfigurationManager.AppSettings["devPassword"];
                client.Token = ConfigurationManager.AppSettings["devToken"];
                client.ClientId = ConfigurationManager.AppSettings["devClientId"];
                client.ClientSecret = ConfigurationManager.AppSettings["devClientSecret"];           
            }

            return client;
        }

        static void Main(string[] args)
        {
            var client = CreateClient();

            if (args.Length > 0)
            {
                client.Login();
                Console.WriteLine(client.Query(args[0]));
            }
            else
            {
                client.Login();
                Console.WriteLine(client.Describe("Account"));
                Console.WriteLine(client.Describe("Contact"));
                Console.WriteLine(client.QueryEndpoints());
                Console.WriteLine(client.Query("SELECT Name from Contact"));
            }
            Console.ReadLine();
        }
    }
}