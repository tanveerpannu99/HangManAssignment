using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Timers;

namespace HangManAssignment
{
    [Activity(Label = "HangManAssignment", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        //Splah screen activity
        ImageView splashLogo;
        private static Timer fadeTimer;
        float fadeAlpha;
        public int count;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //Initialise screen components
            setComponents();
            
            // Create a timer with a 80ms interval.
            fadeTimer = new Timer(80);
            fadeAlpha = 0;
            count = 0;
      
            // Hook up the Elapsed event for the timer. 
            fadeTimer.Elapsed += OnTimedEvent;
            fadeTimer.Enabled = true;

        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            // Create a seperate thread
           RunOnUiThread(() => fadeLogo());
          
        }

        private void fadeLogo()
        {
            //Fade Logo in and out
            if (count < 20)
            {
                fadeAlpha += 0.05f;
                count ++;
            }
            else if(count >=20 && count < 60)
            {
                count++;
            }
            else if (count < 80)
            {
                fadeAlpha -= 0.05f;
                count++;
            }
            else
            {
                loadGame();
            }

            splashLogo.Alpha = fadeAlpha;

        }

        public void setComponents()
        {
            //Initialise screen components
            splashLogo = FindViewById<ImageView>(Resource.Id.imageView_SplashLogo);
            splashLogo.Click += splashLogo_Click;
        }

        void splashLogo_Click(object sender, EventArgs e)
        {
            //If click, bypass logo fade and load activity
            loadGame();
        }

        protected override void OnDestroy()
        {
            //Make sure timer is set off on destroy
            base.OnDestroy();
            fadeTimer.Enabled = false;
        }

        private void loadGame()
        {
            //Start title activity
            fadeTimer.Enabled = false;
            var intent = new Intent(this, typeof(ActivityTitle));
            StartActivity(intent);
            Finish();
        }
    }
}

