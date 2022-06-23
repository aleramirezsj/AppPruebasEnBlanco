using AppPruebasEnBlanco.Core;
using AppPruebasEnBlanco.Models;
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
        public Command GuardarPersonaCommand { get; }
        public Command ModificarPersonaCommand { get; }
        public Command EliminarPersonaCommand { get; }
        public Command RefrescarPersonasCommand { get; }
        private Persona personaSeleccionada;

        public Persona PersonaSeleccionada
        {
            get { return personaSeleccionada; }
            set { personaSeleccionada = value;
                OnPropertyChanged();
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
            GuardarPersonaCommand = new Command(async () =>
            {
                //agregamos a la lista
                personas.Add(
                new Persona()
                {
                    Nombre = nombre,
                    Direccion = direccion
                }
                );
                //limpiamos los Entry
                Nombre = "";
                Direccion = "";


            });
            RefrescarPersonasCommand = new Command(async () =>
            {
                Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Refrescando lista");
            });
            ModificarPersonaCommand = new Command(async () =>
            {
                if(PersonaSeleccionada!=null)
                { 
                    Nombre = PersonaSeleccionada.Nombre;
                    Direccion = PersonaSeleccionada.Direccion;
                    
                }
            });
            EliminarPersonaCommand = new Command(async()=>{
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar persona", "¿Está seguro/a que quiere borrar la personao seleccionada?", "Sí", "No");
                if (respuesta)
                {
                    personas.Remove(PersonaSeleccionada);

                    PersonaSeleccionada = null;
                }
            });


                personas.Add(
                new Persona()
                {
                    Nombre = "Juan Perez",
                    Direccion = "San Martin 1234"
                }
                );
            

        }
    }
}
