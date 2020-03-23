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
{   //Objact class for holding score details
    public class HiScores
    {
        public int ScoreID { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public HiScores()
        {

        }
    }
}