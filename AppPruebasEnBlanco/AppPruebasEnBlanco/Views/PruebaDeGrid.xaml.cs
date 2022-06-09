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
    public partial class PruebaDeGrid : ContentPage
    {
        public PruebaDeGrid()
        {
            InitializeComponent();
        }
        private async void CerrarPruebaDeGrid(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}