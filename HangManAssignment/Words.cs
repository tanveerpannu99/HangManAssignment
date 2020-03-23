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
    //Objact class for holding word details
    public class Words
    {
        public int WordID {get; set;}
        public string Category { get; set; }
        public string Word { get; set; }

        public Words()
        {

        }
    }
}