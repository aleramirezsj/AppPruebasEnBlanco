using AppPruebasEnBlanco.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppPruebasEnBlanco.ViewModels
{
    internal class NuevoUsuarioViewModel : ObjetoNotificacion
    {
        public Command GuardarUsuarioCommand { get; }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }

        private string apellido;
        public string Apellido
        {
            get { return apellido; }
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }
    }
}
