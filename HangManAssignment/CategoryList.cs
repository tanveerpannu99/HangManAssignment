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
    public class CategoryList
    {   //Objact class for holding Category details
         public string Title;
         public int ImageInt;

        public CategoryList()
        {
            Title = "";
            ImageInt = 0;
        }

    }
}