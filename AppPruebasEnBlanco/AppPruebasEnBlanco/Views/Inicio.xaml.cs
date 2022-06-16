using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPruebasEnBlanco.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private async void AbrirPruebaDeGrid(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PruebaDeGrid());
        }

        private async void AbrirControlesComunes(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ControlesComunes());
        }
        private async void AbrirListaPersonas(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaPersonas());
        }
    }
}