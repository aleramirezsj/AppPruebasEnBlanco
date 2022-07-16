using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppPruebasEnBlanco.Core
{
    public class Helper
    {
        public static string ObtenerSha256Hash(string textoAEncriptar)
        {
            // Create a SHA256   
            var sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(textoAEncriptar));

            // Convert byte array to a string   
            StringBuilder hashObtenido = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hashObtenido.Append(bytes[i].ToString("x2"));
            }
            return hashObtenido.ToString();
        }

        public static HttpClient ObtenerClienteHttp()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("x-apikey", "2c677ccf2cb62a940092248e128001983dab0");
            return client;
        }
        public static FirebaseClient ObtenerClienteFB(string dbFB, string FBSecret)
        {
            

            FirebaseClient fc = new FirebaseClient(dbFB,
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FBSecret) });
            return fc;
        }
}
}
