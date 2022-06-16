using AppPruebasEnBlanco.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPruebasEnBlanco.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlesComunes : ContentPage
    {
        ObservableCollection<string> Localidades = new ObservableCollection<string>() { "San Justo", "Ramayón", "Escalada", "Crespo" };
        public ControlesComunes()
        {
            InitializeComponent();
            SldSlider.ValueChanged += (s, e) =>LblSlider.Text = "Slider:" + SldSlider.Value;

            //al cambiar el valor del stepper, hacemos que se muestre su valor actual en la label y además lo reflejamos en la progressbar(dividiendo su Value por 100, dado que la propiedad Progress, espera valores entre 0 y 1)
            StpStepper.ValueChanged += (s, e) =>
            {
                LblStepper.Text = "Stepper:" + StpStepper.Value;
                PgbProgressBar.Progress=StpStepper.Value/100;
            };

            SwtSwitch.Toggled += (s, e) =>
            {
                LblSwitch.Text = "Switch:" + SwtSwitch.IsToggled;
                ActActivity.IsRunning = SwtSwitch.IsToggled;
            };
            pckPicker.ItemsSource = Localidades;
            LstListView.ItemsSource = Localidades;
        }

        private void PonerMayúsculasMinúsculas(object sender, EventArgs e)
        {
            if(BtnMayúsculas.Text==Literals.TextoBotonMayúsculas)
            {
                LblControlesComunes.Text=LblControlesComunes.Text.ToUpper();
                BtnMayúsculas.Text = Literals.TextoBotonMinúsculas;
                ImgLogo.Source = "logoisp20.png";
            }
            else
            {
                LblControlesComunes.Text = LblControlesComunes.Text.ToLower();
                BtnMayúsculas.Text = Literals.TextoBotonMayúsculas;
                ImgLogo.Source = "logoisp20Anterior.jfif";
            }
        }
    }
}