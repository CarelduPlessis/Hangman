using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOverPage : ContentPage
    {
        public GameOverPage()
        {
            InitializeComponent();

            //Message, Entry as GOName, two Lables of GOLevel, GOScore and two Buttons as btnAgain, btnExit.

            BoxView BoxLine = new BoxView
            {

                Color = Color.DarkKhaki

            };

            Label Welcome = new Label
            {
                Text = "Game over!",
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 25
            };
            BoxView BoxLine2 = new BoxView
            {

                Color = Color.DarkKhaki

            };
            Entry GOName = new Entry
            {
                Text = "_",
                FontSize = 25,
                TextColor = Color.Blue,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Entry GOLevel = new Entry
            {
                Text = "_",
                FontSize = 25,
                TextColor = Color.Green,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            int score = 0;
            Label GOscore = new Label
            {
                Text = "Score: " + Convert.ToString(score),
                FontSize = 25,
                TextColor = Color.DarkCyan,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Button btnAgain = new Button
            {
                Text = "Play again!",
                FontSize = 25,
                TextColor = Color.Gray
            };
            btnAgain.Clicked += Again_Clicked;

            Button btnExit = new Button
            {
                Text = "Exit",
                FontSize = 25,
                TextColor = Color.Black
            };
            btnExit.Clicked += Exit_Clicked;

            Content = new StackLayout
            {
                Children =
                {
                    BoxLine,
                    GOName,
                    GOLevel,
                    GOscore,
                    BoxLine2,
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            btnAgain,
                            btnExit,
                        }
                    }
                }
            };
        }
        private void Exit_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void Again_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HangManPage());
        }
        /*public void Again_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LevelPage());
        }*/
    }
}