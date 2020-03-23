using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using System.IO;

namespace HangManAssignment
{
    [Activity(Label = "ActivityCategory", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ActivityCategory : Activity
    {
        static string dbName = "HangManWords.sqlite";
        string dbPath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, dbName);

        ListView listview;
        List<CategoryList> listOfCategories;

        int[] drawables;
        DatabaseManager objDb;
        List<Words> listOfWords;
        string[] categories;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.layoutCategory);

            listview = FindViewById<ListView>(Resource.Id.List);

            //Category icons
            drawables = new int[] { Resource.Drawable.CatFood, Resource.Drawable.CatAnimals, Resource.Drawable.CatHumanBody,
            Resource.Drawable.CatClothes, Resource.Drawable.CatCountries, Resource.Drawable.CatMusicalInstruments, Resource.Drawable.CatMovies,
            Resource.Drawable.CatSongs, Resource.Drawable.CatActors, Resource.Drawable.CatComputers};

            //Copy database to device
            CopyDatabase();

            //Create a new list of type Category
            listOfCategories = new List<CategoryList>();
            //Fill array with categories
            categories = new string[] { "Food", "Animals", "Human Body", "Clothes",
             "Countries", "Musical Instruments", "Movies", "Songs", "Actors", "Computers"};

            for (int i = 0; i < categories.Length; i++)
            {
                //add title and icon to category list
                CategoryList obj = new CategoryList();
                obj.Title = categories[i];
                obj.ImageInt = drawables[i];
                listOfCategories.Add(obj);
            }

            //Link listview on layout with dataadpater
            listview.Adapter = new DataAdapter(this, listOfCategories);

            listview.ItemClick += listview_ItemClick;

        }


        void listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            objDb = new DatabaseManager();
            //Get selected category and rear words from database
            listOfWords = objDb.readWords(categories[e.Position]);

            //Put word in array for passing to activity
            string[] passList = new string[listOfWords.Count];
            for (int i = 0; i < listOfWords.Count; i++)
            {
                passList[i] = listOfWords[i].Word;
            }

            //Pass word to activity
            var intent = new Intent(this, typeof(ActivityGame));
            intent.PutExtra("WordList", passList);
            StartActivity(intent);
            Finish();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Finish();
        }


        public void CopyDatabase()
        {
            //If not exists, copy database to device
            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);

                        }
                    }

                }
            }
        }
    }
}
