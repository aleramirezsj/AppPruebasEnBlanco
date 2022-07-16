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
    public class ListaPersonasFBViewModel : ObjetoNotificacion
    {
        readonly PersonasRepositoryFB personasRepository = new PersonasRepositoryFB();
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
                ActualizarPersonaCommand.ChangeCanExecute();
                EliminarPersonaCommand.ChangeCanExecute();
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
                GuardarPersonaCommand.ChangeCanExecute();
            }
        }
        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value;
                OnPropertyChanged();
                GuardarPersonaCommand.ChangeCanExecute();
            }
        }



        public ListaPersonasFBViewModel()
        {
            personas = new ObservableCollection<Persona>();
            ObtenerPersonasAsync(this);
            GuardarPersonaCommand = new Command(GuardarPersonaClicked,PermitirGuardar);
            RefrescarPersonasCommand = new Command(ObtenerPersonasAsync);
            ModificarPersonaCommand = new Command(ModificarPersonaClicked);
            EliminarPersonaCommand = new Command(EliminarPersonaClicked, PermitirEliminar);
            ActualizarPersonaCommand = new Command(ActualizarPersonaClicked, PermitirActualizar);
 

        }

        private  void ModificarPersonaClicked(object obj)
        {
                Nombre = PersonaSeleccionada.Nombre;
                Direccion = PersonaSeleccionada.Direccion;
        }

        private bool PermitirActualizar(object arg) => personaSeleccionada != null;

        private async void ActualizarPersonaClicked(object obj)
        {
            await personasRepository.ActualizarAsync(personaSeleccionada._id, Nombre, Direccion);
            ObtenerPersonasAsync(this);
            PersonaSeleccionada = null;
            Nombre = "";
            Direccion = "";
        }

        private bool PermitirEliminar(object arg) => personaSeleccionada != null;


        private async void EliminarPersonaClicked(object obj)
        {
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar persona", "¿Está seguro/a que quiere borrar la personao seleccionada?", "Sí", "No");
            if (respuesta)
            {
                await personasRepository.Eliminar(personaSeleccionada._id);
                ObtenerPersonasAsync(this);
                PersonaSeleccionada = null;
                Nombre = "";
                Direccion = "";
            }
        }

        private async void GuardarPersonaClicked(object obj)
        {

            await personasRepository.AgregarAsync(Nombre, Direccion);
            ObtenerPersonasAsync(this);
            //limpiamos los Entry
            Nombre = "";
            Direccion = "";

        }

        private bool PermitirGuardar(object arg) => !string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Direccion);

        private async void ObtenerPersonasAsync(object sender)
        {
            personas.Clear();
            Personas = await personasRepository.ObtenerTodos();

            
        }
    }
}
