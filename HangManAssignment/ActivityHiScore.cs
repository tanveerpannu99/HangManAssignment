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
    [Activity(Label = "ActivityHiScore", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ActivityHiScore : Activity
    {
        string playerName, SQLQuery;
        int playerScore;
        DatabaseManager objDb;
        List<HiScores> hiScores;
        TextView[] txtScores;

        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LayoutHiScore);

            //SET SCREEN COMPONENTS
            setScreen();

            //ENTER NAME AND SCORE TO DATABASE
            writeHiScore();

            //READ HISCORES FROM DATABASE
            readHiScore();

            //DISPLAY HISCORES
            displayHiScores();
            
        }

        private void displayHiScores()
        {
            //Put score into textviews
            string name, score;
            for (int i = 0; i < hiScores.Count; i++)
            {
                name = hiScores[i].Name;
                score = hiScores[i].Score.ToString();
                txtScores[i].Text = name + "  -  " + score;
            }
        }

        private void readHiScore()
        {
            //Read scores from database
            objDb = new DatabaseManager();
            hiScores = objDb.readScores();
        }

        private void writeHiScore()
        {
            //Write scores to database - **Must do BEFORE reading**
            playerScore = Intent.GetIntExtra("Score", 0);
            playerName = Intent.GetStringExtra("Name");

            //ENTER NAME AND SCORE TO DATABASE
            SQLQuery = "INSERT INTO HiScores (Name,Score) VALUES ('" + playerName + "'," + playerScore.ToString() + ")";
            objDb = new DatabaseManager();
            objDb.runQuery(SQLQuery);

        }

        private void setScreen()
        {
            //Initialise screen components
            txtScores = new TextView[10];
            //Get resource id's for textviews
            var res = new int[] { Resource.Id.txtScore1, Resource.Id.txtScore2, Resource.Id.txtScore3, Resource.Id.txtScore4, 
            Resource.Id.txtScore5,Resource.Id.txtScore6,Resource.Id.txtScore7,Resource.Id.txtScore8,Resource.Id.txtScore9,
            Resource.Id.txtScore10};

            for (int i = 0; i < txtScores.Length; i++)
            {   //Find them all
                txtScores[i] = FindViewById<TextView>(res[i]);
            }
        }

        public override void OnBackPressed()
        {
            //RETURN TO MAINMENU ON BACK BUTTON, clear back stack first
            var intent = new Intent(this, typeof(ActivityTitle));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(intent);
            Finish();
        }
    }
}