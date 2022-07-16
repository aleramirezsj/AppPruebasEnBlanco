using AppPruebasEnBlanco.Core;
using AppPruebasEnBlanco.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppPruebasEnBlanco.Repositorys
{
    public class PersonasRepositoryFB
    {
        string dbFB = "https://gestionisp20-default-rtdb.firebaseio.com/";
        string FBSecret = "swq9DDnr0WN0YQKUaFTEauPhoHUsRIvxduGPFhiL";
        public async Task<ObservableCollection<Persona>> ObtenerTodos()
        {
            FirebaseClient clientFB = Helper.ObtenerClienteFB(dbFB, FBSecret);
            //string result = await clientFB.GetStringAsync(Url);
            //return JsonConvert.DeserializeObject<IEnumerable<Persona>>(result);
            var personas = new ObservableCollection<Persona>();

            var ListaPersonas = (await clientFB
                           .Child("TablasPersonas")
                           .OnceAsync<Persona>()).Select(persona => new Persona
                           {
                               Nombre = persona.Object.Nombre,
                               Direccion = persona.Object.Direccion,
                               _id = persona.Key
                           });

            foreach (var persona in ListaPersonas)
            {
                personas.Add(persona);
            }
            return personas;
        }
        public async Task AgregarAsync(string nom, string dire)
        {
            Persona persona = new Persona()
            {
                Nombre = nom,
                Direccion = dire
            };

            FirebaseClient clientFB = Helper.ObtenerClienteFB(dbFB, FBSecret);
            await clientFB.Child("TablasPersonas")
            .PostAsync(new Persona() { Nombre = nom, Direccion = dire });

        }
        public async Task Eliminar(string key)
        {
            FirebaseClient clientFB = Helper.ObtenerClienteFB(dbFB, FBSecret);
            await clientFB.Child("TablasPersonas").Child(key).DeleteAsync();
        }

        public async Task ActualizarAsync(string id,string nom, string dire)
        {
            FirebaseClient clientFB = Helper.ObtenerClienteFB(dbFB, FBSecret);
            await clientFB.Child("TablasPersonas").Child(id)
                    .PutAsync(new Persona() { Nombre = nom, Direccion = dire });


        }
    }
}
