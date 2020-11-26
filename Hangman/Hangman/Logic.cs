using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hangman
{
    //MW's Space
    public class Logic
    {
        //The Hidden Word
        static string HidWord;

        //Defing Labels & Btns

        static int ScoreCount = 0;

        //Gem
        static int GemCount = 0;

        //Total Wrong Guesses
        static int badGuess = 0;
        static int deadNum;
        static int PointsWorth;

        static int GameState;

        /*
        //!!!!!!!!!!!!!!!!!!!!!!!!!! Hard Code !!!!!!!!
        List<string> HMword = new List<string>();
        //Adding to HM Word List

        public void TempHardCode()
        {
            HMword.Add("Siberian Husky");
            HMword.Add("Pirate");
            HMword.Add("Mythical Creature");
            HMword.Add("inevitable");
            HMword.Add("Movie Night");
            HMword.Add("Candy Floss");
            HMword.Add("Wolf");
            HMword.Add("Challenge");
            HMword.Add("Hangman");
            HMword.Add("Ice Cream");
            HMword.Add("Pizza");
            HMword.Add("Hamburger");
            HMword.Add("Soccer");
            HMword.Add("Archery");
            HMword.Add("Horse Riding");
            HMword.Add("To Boldly Go");
            HMword.Add("Cowboy");
            HMword.Add("Valley");
            HMword.Add("Ball");
            HMword.Add("Toy");
            HMword.Add("Chocolate");
            HMword.Add("Nacho");
            HMword.Add("Possible");
            HMword.Add("Crazy");
            HMword.Add("Villan");
            HMword.Add("Hero");
            HMword.Add("Social Butterfly");
            HMword.Add("Nerd");
            HMword.Add("Programmer");
            HMword.Add("Dragon");
            HMword.Add("Pieces of Eight");
            HMword.Add("Map");
            HMword.Add("Internet");
            HMword.Add("Unknown");
            HMword.Add("Dinosaur");
            HMword.Add("Weapon");
            HMword.Add("Pencil");
            HMword.Add("Game");
            HMword.Add("Tired");
            HMword.Add("Bored");
            HMword.Add("Travel");
            HMword.Add("Friends");
            HMword.Add("Star Trek");
            HMword.Add("Irritating");
            HMword.Add("Star");
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!! Hard Code !!!!!!!!


        /*
        //Getting Game Difficulty
        GameMode = Convert.ToChar(Diff);

            if (GameMode == 'E')
            {
                HMpics = "HME";
                deadNum = 12;
                PointsWorth = 1;
            }
            else if (GameMode == 'M')
            {
                HMpics = "HMN";
                deadNum = 9;
                PointsWorth = 3;
            }
            else
            {
                HMpics = "HMH";
                deadNum = 6;
                PointsWorth = 7;
            }

        */

        //Makes the Correct Num of '_' for VisWord
        public static string MakeBlankChars(string HidWord)
        {
            string VisWord = "";

            for (int i = 0; i < HidWord.Length; i++)
            {
                //If it isn't a letter
                if (!Char.IsLetter(Convert.ToChar(HidWord.Substring(i, 1))))
                {
                    VisWord += HidWord.Substring(i, 1);
                }
                else
                {
                    VisWord += "_";
                }
            }

            return VisWord;
        } //Make Blank Chars ENDS

        public static string SpaceString(string MyString)
        {
            string spacedString = "";

            for (int i = 0; i < MyString.Length; i++)
            {
                spacedString += " " + MyString.Substring(i, 1) + " ";
            }

            return spacedString;
        } //Space String ENDS

        public static string RemoveSpaces(string CurrVisWord)
        {
            string shortWord1 = "";
            string shortWord2 = "";

            //Finding Spaces
            for (int i = 0; i < CurrVisWord.Length; i++)
            {
                if (CurrVisWord.Substring(i, 1) == " ")
                {
                    shortWord1 += "0";
                }
                else
                {
                    shortWord1 += CurrVisWord.Substring(i, 1);
                }
            }

            //Conserving a Word Space
            shortWord1 = shortWord1.Replace("000", " ");

            //Places Word Space Bck
            for (int i = 0; i < shortWord1.Length; i++)
            {
                if (shortWord1.Substring(i, 1) == "0")
                {
                    shortWord2 += "";
                }
                else
                {
                    shortWord2 += shortWord1.Substring(i, 1);
                }
            }

            return shortWord2;
        } //Remove Spaces ENDS

        public static void NewHMGame(Label ScoreTxt, Image HMimg, Label VisWordTxt, Button[] AlphaBtns, Button GemBtn) //Sets Up Hangman Game
        {
            //Showing the Total Score & Gems
            ScoreTxt.Text = Convert.ToString(ScoreCount);
            GemBtn.Text = "X " + GemCount;

            //Getting New Word
            //!!!!!!!!!!!!!!!!!!!!!!!!!! Hard Code !!!!!!!!
            HidWord = "debugging";
            //!!!!!!!!!!!!!!!!!!!!!!!!!! Hard Code !!!!!!!!

            //Ensures Letters are ALL Capitals
            HidWord = HidWord.ToUpper();

            //Showing the Visable Word Letter Count
            VisWordTxt.Text = SpaceString(MakeBlankChars(HidWord));

            //Enabling Gems 1++
            if (GemCount > 0)
            {
                GemBtn.IsEnabled = true;
                GemBtn.BackgroundColor = Color.White;
            }
            else //Disabling Gems 0
            {
                GemBtn.IsEnabled = false;
                GemBtn.BackgroundColor = Color.LightGray;
            }

            //Re-Enabling ALL Alphabet Btns
            for (int i = 0; i < AlphaBtns.Length; i++)
            {
                AlphaBtns[i].IsEnabled = true;
                AlphaBtns[i].BackgroundColor = Color.White;
            }

            //Showing Img, Score & set bad Guesses to Zero
            // + HMpics
            HMimg.Source = "HME" + 1 + ".png";
            ScoreTxt.Text = "Score: " + ScoreCount;
            badGuess = 0;

            //New Game Starts
            GameState = 1;
        } //NewHMGame ENDS

        public static async void GuessChar(object sender, EventArgs e, Label ScoreTxt, Image HMimg, Label VisWordTxt, Button[] AlphaBtns, Button GemBtn)
        {
            //Only While Game is Active
            if (sender is Button btn && GameState == 1)
            {
                btn.IsEnabled = false;

                //The Char Exists in HidWord
                if (HidWord.Contains(btn.Text) == true)
                {
                    //Background is greenish for correct input
                    btn.BackgroundColor = Color.FromRgb(223, 236, 223);

                    string NewVisWord = "";
                    string VisWord = RemoveSpaces(VisWordTxt.Text);

                    for (int i = 0; i < HidWord.Length; i++)
                    {
                        if (HidWord.Substring(i, 1) == btn.Text)
                        {
                            NewVisWord += btn.Text;
                        }
                        else
                        {
                            NewVisWord += VisWord.Substring(i, 1);
                        }
                    }

                    VisWordTxt.Text = SpaceString(NewVisWord);

                    //If all Letters are Found
                    if (NewVisWord.Contains("_") == false)
                    {
                        //Game Over
                        GameState = 0;
                        GameEnd(1, ScoreTxt, HMimg, VisWordTxt, AlphaBtns, GemBtn);
                    }

                } //The Char Exists in HidWordENDS
                else
                {
                    badGuess++;

                    //No Lives are left
                    if ((badGuess + 1) == deadNum)
                    {
                        HMimg.Source = "HMDead.png";
                        VisWordTxt.Text = SpaceString(HidWord);

                        //Game Over
                        GameState = 0;
                        GameEnd(0, ScoreTxt, HMimg, VisWordTxt, AlphaBtns, GemBtn);

                        //Background is redish for incorrect input
                        btn.BackgroundColor = Color.FromRgb(255, 102, 102);

                        //Dead Char Changes to badGuess Color
                        await Task.Delay(1000);
                        btn.BackgroundColor = Color.FromRgb(236, 223, 223);
                    }
                    else
                    {
                        HMimg.Source = "HME" + (badGuess + 1) + ".png";

                        //Background is redish for incorrect input
                        btn.BackgroundColor = Color.FromRgb(236, 223, 223);
                    }
                }
            }
        } //GuessChar ENDS

        /*
        public static void UseGem(object sender, EventArgs e)
        {
            //Only While Game is Active
            if (sender is Button GemBtn && GameState == 1 && GemCount > 0)
            {
                //Uses A Gem
                GemCount = GemCount - 1;
                GemBtn.Text = "X " + GemCount;

                //Giving Hint
                string PossHint = "";

                string NewVisWord = "";

                //Getting Not Used Letters
                //Safety Measure for For Loop
                if (HidWord.Length == VisWord.Length)
                {
                    for (int i = 0; i < HidWord.Length; i++)
                    {
                        //If Characters is still Unknown and not a current PossHint
                        if (VisWord.Substring(i, 1) == "_" && PossHint.Contains(VisWord.Substring(i, 1)) == false)
                        {
                            PossHint += HidWord.Substring(i, 1);
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Error: Hidden Word and Visable Word Lengths Are Different");
                }
                //Getting Not Used Letters ENDS

                //Selecting Hint Character
                PossHint = PossHint.Substring(luck.Next(PossHint.Length), 1);

                //Revealing ALL of Character
                if (HidWord.Length == VisWord.Length)
                {
                    for (int i = 0; i < HidWord.Length; i++)
                    {
                        if (HidWord.Substring(i, 1) == PossHint)
                        {
                            NewVisWord += PossHint;
                        }
                        else
                        {
                            NewVisWord += VisWord.Substring(i, 1);
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Error: Hidden Word and Visable Word Lengths Are Different");
                }
                //Revealing ALL of Character ENDS

                VisWord = NewVisWord;
                label.Text = SpaceString(VisWord);

                //Disabling Character's Btn
                int CharValue = Convert.ToInt32(Convert.ToChar(PossHint)) - 65;
                btns[CharValue].IsEnabled = false;
                btns[CharValue].BackgroundColor = Color.FromRgb(223, 236, 223);
                //Giving Hint ENDS

                //If all Letters are Found
                if (VisWord.Contains("_") == false)
                {
                    //Game Over
                    GameState = 0;
                    GameEnd(1);
                }

                //Disables Btn if No Gems Left
                if (GemCount <= 0)
                {
                    GemBtn.IsEnabled = false;
                    GemBtn.BackgroundColor = Color.LightGray;
                }


            }
        } //UseGem ENDS

        */
        public static async void GameEnd(int gameResult, Label ScoreTxt, Image HMimg, Label VisWordTxt, Button[] AlphaBtns, Button GemBtn)
        {
            //Shows Game Result
            await GameResult(gameResult, ScoreTxt, HMimg, VisWordTxt, AlphaBtns, GemBtn);

            //New Hangman Game
            if (gameResult == 1)
            {
                NewHMGame(ScoreTxt, HMimg, VisWordTxt, AlphaBtns, GemBtn);
            }
            else //GAME OVER
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!
            }
        }


        //Little Fun Result 'Animation'
        public static async Task GameResult(int gameResult, Label ScoreTxt, Image HMimg, Label VisWordTxt, Button[] AlphaBtns, Button GemBtn)
        {
            int GemsEarned = 0;

            if (gameResult == 1)
            {
                HidWord = "You Win!";

                //Getting this Rounds Total Points
                int RoundPoints = ((deadNum - 1) - badGuess) * PointsWorth;

                //Adding to the Total Score
                ScoreCount = ScoreCount + RoundPoints;

                //Adding to Users Gems
                GemsEarned = RoundPoints / 10;
                GemCount = GemCount + GemsEarned;
            }
            else
            {
                HidWord = "Game Over";
            }
            //Ensures Capitals are Used
            HidWord = HidWord.ToUpper();


            //Gives User Time to See the HM Word
            await Task.Delay(1000);

            //Showing Gems Earned
            if (GemsEarned > 0)
            {
                switch (GemsEarned)
                {
                    case 1: HMimg.Source = "HMGem.png"; break;
                    case 2: HMimg.Source = "HMGem2.png"; break;
                    case 3: HMimg.Source = "HMGem3.png"; break;
                }

                ScoreTxt.Text = "+ " + GemsEarned;
            }

            //Reveals New Word
            string VisWord = SpaceString(MakeBlankChars(HidWord));
            VisWordTxt.Text = VisWord;

            //Reveal Results Character by Character
            for (int i = 1; i < HidWord.Length; i++)
            {
                //Only Wait to Reveal Characters
                if (Char.IsLetter(Convert.ToChar(HidWord.Substring((i - 1), 1))))
                {
                    await Task.Delay(200);
                }

                VisWordTxt.Text = SpaceString(HidWord.Substring(0, i) + VisWord.Substring(i, (HidWord.Length - i)));
            }

            await Task.Delay(200);
            VisWordTxt.Text = SpaceString(HidWord);

            //Gives User Time to See Result
            await Task.Delay(1000);
        }
    }
}


