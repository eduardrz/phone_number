using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MiPrimerApp
{
    [Activity(Label = "Numero Telefonico", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EditText TextoTelefono = FindViewById<EditText>(Resource.Id.TelText);
            Button translateButton = FindViewById<Button>(Resource.Id.btnTelefono);
            Button callButton = FindViewById<Button>(Resource.Id.btnLlamar);

            callButton.Enabled = false;

            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>
            {
                translatedNumber = Core.PhoneWordTranslator.ToNumber(TextoTelefono.Text);
                if (String.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = true;

                }
                else
                {
                    callButton.Text = "Llamar a: " + translatedNumber;
                    callButton.Enabled = true;
                }
            };
            callButton.Click +=(object sender, EventArgs e) =>
            {
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("¿Llamar al numero? " + translatedNumber);
                callDialog.SetNeutralButton("Llamar", delegate
                {
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Android.Net.Uri.Parse("Tel:" + translatedNumber));
                    StartActivity(callIntent);
                });
                callDialog.SetNegativeButton("Cancelar", delegate { });

                callDialog.Show();
            };
        }

       
    } 
    
}

