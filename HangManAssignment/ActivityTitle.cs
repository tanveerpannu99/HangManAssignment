using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;

namespace HangManAssignment
{
    [Activity(Label = "ActivityTitle", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ActivityTitle : Activity
    {
        ImageView btnPlay;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.layoutTitle);

            //Initialise screen components
            btnPlay = FindViewById<ImageView>(Resource.Id.btnPlay);
            btnPlay.Click += btnPlay_Click;

        }

        void btnPlay_Click(object sender, EventArgs e)
        {
            //Start Activity on button push
            var intent = new Intent(this, typeof(ActivityCategory));
            StartActivity(intent);
        }
    }
}