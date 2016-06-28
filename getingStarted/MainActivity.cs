using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace getingStarted
{
    [Activity(Label = "getingStarted", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        EditText phoneNumberText;
        Button translateButton;
        Button callButton;
       

// Add code to translate number
string translatedNumber = string.Empty;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNamber);
             translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
             callButton = FindViewById<Button>(Resource.Id.CallButton);
            translateButton.Click += TranslateButton_Click;
            callButton.Click += CallButton_Click;
            callButton.Enabled = false;
        }

        private void CallButton_Click(object sender, EventArgs e)
        { 
            // On "Call" button click, try to dial phone number.
            var callDialog = new AlertDialog.Builder(this);
            callDialog.SetMessage("Call " + translatedNumber + "?");
            callDialog.SetNeutralButton("Call", delegate {
                // Create intent to dial phone
                var callIntent = new Intent(Intent.ActionCall);
                callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                StartActivity(callIntent);
            });
            callDialog.SetNegativeButton("Cancel", delegate { });

            // Show the alert dialog to the user and wait for response.
            callDialog.Show();

        }

        private void TranslateButton_Click(object sender, EventArgs e)
        {

            translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
            if (String.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.Text = "Call";
                callButton.Enabled = false;
            }
            else
            {
                callButton.Text = "Call " + translatedNumber;
                callButton.Enabled = true;
            }
        }
    }
}

