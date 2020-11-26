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
        // Setting public int.
        public int score = 0;
        public GameOverPage()
        {
            InitializeComponent();

            //Message, Labels as GOName, GOLevel, GOScore as 0.

            // Color line.
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
                FontSize = 40
            };

            // Color line.
            BoxView BoxLine2 = new BoxView
            {

                Color = Color.DarkKhaki

            };
            Label GOName = new Label
            {
                Text = "_",
                FontSize = 25,
                TextColor = Color.Blue,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Label GOLevel = new Label
            {
                Text = "_",
                FontSize = 25,
                TextColor = Color.Green,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Label GOscore = new Label
            {
                Text = "Score: " + Convert.ToString(score),
                FontSize = 25,
                TextColor = Color.DarkCyan,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // Setting two Buttons as btnAgain, btnExit.
            Button btnAgain = new Button
            {
                Text = "Play again!",
                FontSize = 25,
                TextColor = Color.Red
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
                    Welcome,
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

        // Button navigations
        // Exit button to kill
        private void Exit_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        // Again button to get back to LevelPage
        public void Again_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LevelPage());
        }
    }
}
