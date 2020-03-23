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

namespace HangManAssignment
{
    [Activity(Label = "PopWindow", Theme = "@style/Theme.PopTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class PopWindow : Activity
    {
        //Activity for pop up window - uses custom theme PopTheme
        
        int playerScore;
        string playerAnwser;
        string playerName;
        TextView score;
        TextView anwser;
        Button ok;
        TextView name;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.layoutPopUp);

            //Initialise screen components
            score = FindViewById<TextView>(Resource.Id.txtScore);
            anwser = FindViewById<TextView>(Resource.Id.txtAnswer);
            ok = FindViewById<Button>(Resource.Id.btnOK);
            name = FindViewById<TextView>(Resource.Id.editName);

            //Ok button click listener
            ok.Click += ok_Click;
            //Listener for checking the enter key pushed
            name.KeyPress += name_KeyPress;

            //Screen size variables
            var metrics = Resources.DisplayMetrics;
            int width = metrics.WidthPixels;
            int height = metrics.HeightPixels;
            
            //Set with to 80% and height to 60% of screen
            this.Window.SetLayout((int)(width*0.8), (int)(height*0.6));

            //Get score from previous activity
            playerScore = Intent.GetIntExtra("HiScore", 0);
            //Show Score
            score.Text = "Score: " + playerScore.ToString();
            //Get and Show the anwser they got wrong
            playerAnwser = Intent.GetStringExtra("LastWord");
            anwser.Text = playerAnwser;

        }

        void name_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            {
                //If return key pressed load next ativity so they dont have to press the ok button
                sendHiScore();
                e.Handled = true;
            }

        }

        public override void OnBackPressed()
        {
            //If back key pushed, clear back stack and go back to main menu
            var intent = new Intent(this, typeof(ActivityTitle));
            intent.SetFlags(ActivityFlags.NewTask|ActivityFlags.ClearTask);
            StartActivity(intent);
            Finish();
        }

        void ok_Click(object sender, EventArgs e)
        {
            //On ok push, load next ativity
            sendHiScore();
        }

        private void sendHiScore()
        {
            //If no name entered, set a default
            if (name.Text == "")
                playerName = "John Doe";
            else
                playerName = name.Text;  //Otherwise use what they entered

            //Send it to the Hi Score activity
            var intent = new Intent(this, typeof(ActivityHiScore));
            intent.PutExtra("Score", playerScore);
            intent.PutExtra("Name", playerName);
            StartActivity(intent);
            Finish();
        }

    }
}