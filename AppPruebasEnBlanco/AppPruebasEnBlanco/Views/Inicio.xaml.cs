using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

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
        private async void AbrirListaPersonasFB(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaPersonasFB());
        }
        async void EscanearQr(object sender, System.EventArgs e)
        {
            MobileBarcodeScanningOptions options = new MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
            ZXing.BarcodeFormat.PDF_417
        };
            options.TryHarder = true;

            var scanPage = new ZXingScannerPage(options);


            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () => {
                    await Navigation.PopAsync();
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };

            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
        }

        CameraResolution HandleCameraResolutionSelectorDelegate(List<CameraResolution> availableResolutions)
        {
            //Don't know if this will ever be null or empty
            if (availableResolutions == null || availableResolutions.Count < 1)
                return new CameraResolution() { Width = 800, Height = 600 };

            //Debugging revealed that the last element in the list
            //expresses the highest resolution. This could probably be more thorough.
            return availableResolutions[availableResolutions.Count - 1];
        }
    }
}