using System;
using System.IO;
using System.Net;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Newtonsoft.Json.Linq;

namespace Tema3
{
    class Program
    {

        private static DriveService _service;
        private static string _token;

        static void Main(string[] args)
        {
            init();
        }

    static void init()
    {
        string[] scop = new string[]
        {
            DriveService.Scope.Drive,
            DriveService.Scope.DriveFile
        };

        var clientId="914728126287-co0g1dc07h6sfnq2fqchd3pl1i6ovv4u.apps.googleusercontent.com";
        var clientSecret="aUPmh5BMSJjdXrTTDlmNrDB-";
        
           var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                scop,
                Environment.UserName,
                CancellationToken.None,

                null

            ).Result;

        _service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
    _token=credential.Token.AccessToken;


    Console.Write("Token: "+credential.Token.AccessToken);


    }

    }
}
