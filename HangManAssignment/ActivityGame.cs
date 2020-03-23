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
using Android.Graphics;

namespace HangManAssignment
{
    [Activity(Label = "ActivityGame", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ActivityGame : Activity
    {    
        ImageView[] alphaButtons;
        int[] hangmanDrawables;
        int[] drawables;
        int[] res;

        LinearLayout anwserLayout;

        string word;
        string[] wordList;
        ImageView[] lettersForAnwser;
        ImageView imgHangman;
        TextView playerScore;
        int loseCount = 0;
        int winCount;
        int score = 0;
        public bool pressedCancel = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.layoutGame);

            //Find all elements in layout and set up layout
            viewSetup();

            ////GET WORD FROM DATABASE
            getWord();

            setLetters();

        }
        
        private void setLetters()
        {
            ////SET Screen
            //make a list of image views to the length of the letters in the word
            lettersForAnwser = new ImageView[word.Length];
            //Draw blank letters
            for (int i = 0; i < word.Length; i++)
            {
                lettersForAnwser[i] = new ImageView(this);

                if (word[i] == Convert.ToChar(" "))  //Skip spaces
                {
                    lettersForAnwser[i].SetImageResource(Resource.Drawable.L_BlankSpace);
                }
                else
                {
                    lettersForAnwser[i].SetImageResource(Resource.Drawable.L_Blank);
                    lettersForAnwser[i].SetColorFilter(Color.Argb(70, 255, 0, 0));
                }
                    
                LinearLayout.LayoutParams parameters = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.WrapContent);
                parameters.Weight = 1;

                anwserLayout.AddView(lettersForAnwser[i],parameters);
                
            }
        }

        private void CheckLetter(string letter, int position)
        {
            //If pressed letter is in current word
            if (word.Contains(letter))
            {
                //Loop through each letter in the word
                for (int i = 0; i < word.Length; i++)
                {
                    //If letter (as character in position 0 is always the letter)
                    // is equal to current letter in the word
                    if (letter[0] == word[i]){
                        //Change the ImageView image to the correct letter
                        //lettersForAnwser[i] = current blank imageview index
                        //drawables[position] = drawable list in selected position
                        lettersForAnwser[i].SetImageResource(drawables[position]);
                        winCount--;
                    }
                }
            }
            else
            {
                //LETTER NOT IN WORD
                //DRAW HANGMAN
                loseCount++;
                imgHangman.SetImageResource(hangmanDrawables[loseCount]);
            }

            if(loseCount > 7)
            {   //HAVE LOST - BUNNY HUNG - POOR BUNNY - DO LOSE STUFF
                lose();             
            }

            if(winCount < 1)
            {   //HAVE WON - BUNNY SAVED - YOU HERO - DO WIN STUFF
                win();
            }

        }

        private void getWord()
        {
            wordList = Intent.GetStringArrayExtra("WordList");
            newWord();
        }

        private void newWord()
        {
            ///CHOOSE RANDOM WORD
            Random randomWord = new Random(DateTime.Now.Millisecond);
            word = wordList[randomWord.Next(0, wordList.Length)];
            winCount = word.Length;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == Convert.ToChar(" "))
                    winCount--;
            }
        }

        private void win()
        {
            //ADD POINTS
            score += 150;
            //DISPLAY POINTS
            playerScore.Text = score.ToString();
            //SHOW LAST WORD
            Toast.MakeText(this, word + ". Well Done!", ToastLength.Long).Show();
            //NEW WORD
            loseCount = 0;
            imgHangman.SetImageResource(hangmanDrawables[0]);
            for (int i = 0; i < word.Length; i++)
            {
                anwserLayout.RemoveView(lettersForAnwser[i]);
            }
            for (int i = 0; i < alphaButtons.Length; i++)
            {
                alphaButtons[i].Clickable = true;
                alphaButtons[i].Alpha = 1f;
            }

            //get new word
            newWord();
            //set blank letter for word length
            setLetters();
       
        }

        private void lose()
        {
            //If lose, show pop up, send data
            var intent = new Intent(this, typeof(PopWindow));
            intent.PutExtra("HiScore", score);
            intent.PutExtra("LastWord", word);
            StartActivity(intent);
        }

        public void viewSetup()
        {
            //Initialise screen components
            anwserLayout = FindViewById<LinearLayout>(Resource.Id.LinWord);
            imgHangman = FindViewById<ImageView>(Resource.Id.imgHangman);
            playerScore = FindViewById<TextView>(Resource.Id.txtScore);

            //Get resource id from each image of letter
            drawables = new int[] { Resource.Drawable.L_A, Resource.Drawable.L_B, Resource.Drawable.L_C, 
            Resource.Drawable.L_D, Resource.Drawable.L_E, Resource.Drawable.L_F, Resource.Drawable.L_G, 
            Resource.Drawable.L_H, Resource.Drawable.L_I, Resource.Drawable.L_J, Resource.Drawable.L_K, 
            Resource.Drawable.L_L, Resource.Drawable.L_M, Resource.Drawable.L_N, Resource.Drawable.L_O, 
            Resource.Drawable.L_P, Resource.Drawable.L_Q, Resource.Drawable.L_R, Resource.Drawable.L_S, 
            Resource.Drawable.L_T, Resource.Drawable.L_U, Resource.Drawable.L_V, Resource.Drawable.L_W,
            Resource.Drawable.L_X, Resource.Drawable.L_Y, Resource.Drawable.L_Z};

            //Get resource id from each image of hangman
            hangmanDrawables = new int[] { Resource.Drawable.Hangman1, Resource.Drawable.Hangman2,
            Resource.Drawable.Hangman3,Resource.Drawable.Hangman4,Resource.Drawable.Hangman5,
            Resource.Drawable.Hangman6,Resource.Drawable.Hangman7,Resource.Drawable.Hangman8,
            Resource.Drawable.Hangman9};

            //Get resource id from each imageview
            res = new int[] { Resource.Id.btn_A, Resource.Id.btn_B, Resource.Id.btn_C, Resource.Id.btn_D, 
            Resource.Id.btn_E, Resource.Id.btn_F, Resource.Id.btn_G, Resource.Id.btn_H, Resource.Id.btn_I, 
            Resource.Id.btn_J, Resource.Id.btn_K, Resource.Id.btn_L, Resource.Id.btn_M, Resource.Id.btn_N, 
            Resource.Id.btn_O, Resource.Id.btn_P, Resource.Id.btn_Q, Resource.Id.btn_R, Resource.Id.btn_S, 
            Resource.Id.btn_T, Resource.Id.btn_U, Resource.Id.btn_V, Resource.Id.btn_W, Resource.Id.btn_X, 
            Resource.Id.btn_Y, Resource.Id.btn_Z };

            //Create the button image views
            alphaButtons = new ImageView[26];

            for (int i = 0; i < alphaButtons.Length; i++)
            {   //find them all
                alphaButtons[i] = FindViewById<ImageView>(res[i]);
            }

            foreach(ImageView iv in alphaButtons)
            {   //set up all the clicks
                iv.Click += iv_Click;
            }


        }

        void iv_Click(object sender, EventArgs e)
        {
            //for any button push
            var pressbutton = sender as ImageView;
            //get tag of sender
            string s = pressbutton.Tag.ToString();
            //work out image for button pushed by using the letter tag - 97 so A=0 etc
            //use this as an index for images
            int charPosition = (int)s[0]-97;
            alphaButtons[charPosition].Clickable = false;
            alphaButtons[charPosition].Alpha = 0.3f;
            //check if letter is in word
            CheckLetter(s, charPosition);
        }

    }
}