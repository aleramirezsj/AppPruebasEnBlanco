using AppPruebasEnBlanco.Core;
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
    public partial class ControlesComunes : ContentPage
    {
        public ControlesComunes()
        {
            InitializeComponent();
            SldSlider.ValueChanged += (s, e) =>
              {
                  LblSlider.Text = "Slider:" + SldSlider.Value;
              };
            StpStepper.ValueChanged += (s, e) =>
              {
                  LblStepper.Text = "Stepper:" + StpStepper.Value;
              };
        }

        private void PonerMayúsculasMinúsculas(object sender, EventArgs e)
        {
            if(BtnMayúsculas.Text==Literals.TextoBotonMayúsculas)
            {
                LblControlesComunes.Text=LblControlesComunes.Text.ToUpper();
                BtnMayúsculas.Text = Literals.TextoBotonMinúsculas;
            }
            else
            {
                LblControlesComunes.Text = LblControlesComunes.Text.ToLower();
                BtnMayúsculas.Text = Literals.TextoBotonMayúsculas;
            }
        }
    }
}