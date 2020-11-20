using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Hangman
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Message and five buttons as btnGame, btnInst, btnProf, btnDB, btnExit.
            Label Welcome = new Label
            {
                Text = "Welcome to Hangman game!",
                TextColor = Color.Blue,
                FontSize = 25
            };
            Button btnGame = new Button
            {
                Text = "Start the Game!",
                FontSize = 25,
                TextColor = Color.Red
            };
            btnGame.Clicked += StartGame_Clicked;
            Button btnInst = new Button
            {
                Text = "Instructions",
                FontSize = 25,
                TextColor = Color.Green
            };
            btnInst.Clicked += Instruction_Clicked;
            Button btnProf = new Button
            {
                Text = "Profile",
                FontSize = 25,
                TextColor = Color.Yellow
            };
            btnProf.Clicked += Profile_Clicked;
            Button btnDB = new Button
            {
                Text = "DB / Extra",
                FontSize = 25,
                TextColor = Color.Gray
            };
            btnDB.Clicked += DB_Clicked;
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
                    Welcome,
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            btnGame,
                            btnInst,
                            btnProf,
                            btnDB,
                            btnExit,
                        }
                    }
                }
            };
        }
        private void Instruction_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InstructionPage());
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void DB_Clicked(object sender, EventArgs e)
        {
            //DBPage will be created.
            //Navigation.PushAsync(new DBPage());
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void StartGame_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LevelPage());
        }



    }
}
