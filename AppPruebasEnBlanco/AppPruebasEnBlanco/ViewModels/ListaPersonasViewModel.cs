using AppPruebasEnBlanco.Core;
using AppPruebasEnBlanco.Models;
using AppPruebasEnBlanco.Repositorys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace AppPruebasEnBlanco.ViewModels
{
    public class ListaPersonasViewModel : ObjetoNotificacion
    {
        readonly PersonasRepository personasRepository = new PersonasRepository();
        public Command GuardarPersonaCommand { get; }
        public Command ModificarPersonaCommand { get; }
        public Command EliminarPersonaCommand { get; }
        public Command RefrescarPersonasCommand { get; }
        public Command ActualizarPersonaCommand { get; }
        private Persona personaSeleccionada;

        public Persona PersonaSeleccionada
        {
            get { return personaSeleccionada; }
            set { personaSeleccionada = value;
                OnPropertyChanged();
                if(personaSeleccionada!=null)
                    ModificarPersonaCommand.Execute(this);
                
            }
        }

        private ObservableCollection<Persona> personas;

        public ObservableCollection<Persona> Personas
        {
            get { return personas; }
            set { personas = value;
                OnPropertyChanged();
            }
        }


        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value;
                OnPropertyChanged();
            }
        }
        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value;
                OnPropertyChanged();
            }
        }



        public ListaPersonasViewModel()
        {
            personas = new ObservableCollection<Persona>();
            ObtenerPersonasAsync(this, new EventArgs());
            GuardarPersonaCommand = new Command(async () =>
            {
                _ = personasRepository.AgregarAsync(Nombre, Direccion);
                ObtenerPersonasAsync(this, new EventArgs());
                //limpiamos los Entry
                Nombre = "";
                Direccion = "";
    

            });
            RefrescarPersonasCommand = new Command(async () =>
            {
                ObtenerPersonasAsync(this, new EventArgs());
            });
            ModificarPersonaCommand = new Command(async () =>
            {
                Nombre = PersonaSeleccionada.Nombre;
                Direccion = PersonaSeleccionada.Direccion;
            }
            );
            EliminarPersonaCommand = new Command(execute: async()=>{
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar persona", "¿Está seguro/a que quiere borrar la personao seleccionada?", "Sí", "No");
                if (respuesta)
                {
                    _ = personasRepository.Eliminar(personaSeleccionada._id);
                    ObtenerPersonasAsync(this, new EventArgs());

                    PersonaSeleccionada = null;
                }
            },
                canExecute: () => personaSeleccionada != null
            );
            ActualizarPersonaCommand = new Command(execute:async () =>
            {
                _ = personasRepository.ActualizarAsync(personaSeleccionada._id, Nombre, Direccion);
                ObtenerPersonasAsync(this, new EventArgs());
                PersonaSeleccionada = null;
            },
            canExecute: () => personaSeleccionada != null
            );


                
            

        }

        private async void ObtenerPersonasAsync(object sender, EventArgs e)
        {
            
            personas.Clear();

            var personasCollection = await personasRepository.ObtenerTodos();

            foreach (Persona persona in personasCollection)
            {
                personas.Add(persona);
            }
        }
    }
}
