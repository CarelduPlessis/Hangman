using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography.X509Certificates;
using System.Windows;


namespace Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HangManPage : ContentPage
    {
        // Setting public int and string.
        public int score = 0;
        public int attempt = 0;
        public int HMpicture = 1;
        public string word = "_";

        Button btn;
        Button[] btns = new Button[26];

        public HangManPage(string Diff)
        {
            InitializeComponent();

            // Setting score as GScore, attempt as GAttempt, Hangman picture as HMImage, word as letterLabel, then keyborad and HMGem at bottom right.

            Grid myGrid = new Grid();

            BoxView scoreBox = new BoxView
            {
                Color = Color.Purple
            };
            Grid.SetRow(scoreBox, 0);
            Grid.SetColumnSpan(scoreBox, 7);

            Label GScore = new Label
            {
                Text = "Score: " + Convert.ToString(score),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Cyan,
                FontSize = 30
            };
            Grid.SetRow(GScore, 0);
            Grid.SetColumnSpan(GScore, 7);

            Label GameLbl = new Label
            {
                Text = "Attempt: " + Convert.ToString(attempt),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Cyan,
                FontSize = 20
            };
            Grid.SetRow(GameLbl, 0);
            Grid.SetColumnSpan(GameLbl, 7);

            BoxView imageBox = new BoxView
            {
                Color = Color.Black
            };
            Grid.SetRow(imageBox, 1);
            Grid.SetRowSpan(imageBox, 3);
            Grid.SetColumnSpan(imageBox, 7);

            Image HMimage = new Image
            {
                Source = "HM0" + Convert.ToString(HMpicture) + ".png",
            };
            Grid.SetRow(HMimage, 1);
            Grid.SetRowSpan(HMimage, 3);
            Grid.SetColumnSpan(HMimage, 7);

            BoxView letterBox = new BoxView
            {
                Color = Color.Purple
            };
            Grid.SetRow(letterBox, 4);
            Grid.SetColumnSpan(letterBox, 7);

            Label letterLabel = new Label
            {
                Text = word,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Cyan,
                FontSize = 30
            };
            Grid.SetRow(letterLabel, 4);
            Grid.SetColumnSpan(letterLabel, 7);

            Button HMGem = new Button
            {
                Text = "HMGem.png",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
            };
            Grid.SetRow(HMGem, 8);
            Grid.SetColumnSpan(HMGem, 7);

            // Keyboard setting.
            int letter = 65;
            //Value = A
            int letterZ = 90;
            char MyChar;
            int btnsIndex = 0;

            //Rows
            for (int r = 5; letter <= letterZ; r++)
            {
                //Columns
                for (int c = 0; c < 7 && letter <= letterZ; c++)
                {
                    MyChar = Convert.ToChar(letter);
                    //myGrid.Children.Add(new BoxView
                    //{
                    //    Margin = 0,
                    //    HeightRequest = 40
                    //}, c, r);
                    myGrid.Children.Add(btn = new Button
                    {
                        Text = Convert.ToString(MyChar),
                        FontSize = 20
                    }, c, r);
                    //MN - Added
                    btns[btnsIndex] = btn;
                    btnsIndex++;
                    btn.Clicked += (object sender, EventArgs e) =>
                    {
                        Logic.GuessChar(sender, e, GScore, GameLbl, HMimage, letterLabel, btns, HMGem);
                    };
                    //MN - Added ENDS

                    letter++;
                }
            }
            //MyChar.Clicked += OnButtonClicked;

            myGrid.Children.Add(scoreBox);
            myGrid.Children.Add(GameLbl);
            myGrid.Children.Add(GScore);
            myGrid.Children.Add(imageBox);
            myGrid.Children.Add(HMimage);
            myGrid.Children.Add(letterBox);
            myGrid.Children.Add(letterLabel);
            myGrid.Children.Add(HMGem);

            Content = myGrid;

            //MN - Setup Game Difficulty
            //DB db = new DB();
            //db.readdata
            Logic.SetDiff(Diff);

            //Loads HM Game once on load
            Logic.NewHMGame(GScore, GameLbl, HMimage, letterLabel, btns, HMGem);
            //MN - ENDS
        }
    }
}
