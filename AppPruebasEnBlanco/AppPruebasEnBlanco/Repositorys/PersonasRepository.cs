using AppPruebasEnBlanco.Core;
using AppPruebasEnBlanco.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppPruebasEnBlanco.Repositorys
{
    public class PersonasRepository
    {
        const string Url = "https://biblioisp20-92ed.restdb.io/rest/personas";

        public async Task<IEnumerable<Persona>> ObtenerTodos()
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Persona>>(result);
        }
        public async Task<Persona> AgregarAsync(string nom, string dire)
        {
            Persona persona = new Persona()
            {
                Nombre = nom,
                Direccion = dire
            };

            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(persona),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Persona>(
                await response.Content.ReadAsStringAsync());
        }
        public async Task Eliminar(string id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            await client.DeleteAsync(Url + "/" + id);
        }

        public async Task ActualizarAsync(string id,string nom, string dire)
        {
            Persona persona = new Persona()
            {
                _id=id,
                Nombre = nom,
                Direccion = dire
            };

            HttpClient client = Helper.ObtenerClienteHttp();
             await client.PutAsync(Url + "/" + persona._id,
                new StringContent(
                    JsonConvert.SerializeObject(persona),
                    Encoding.UTF8, "application/json"));

            
        }
    }
}
