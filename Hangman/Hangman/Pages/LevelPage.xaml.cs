using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hangman.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelPage : ContentPage
    {
        public LevelPage()
        {
            InitializeComponent();

            StackLayout stack = new StackLayout();

            Button btnEasy = new Button();
            Button btnMed = new Button();
            Button btnHard = new Button();

            //btnEasy.Text = "Easy";
            //btnEasy.Clicked += BtnEasy_Clicked;
            //btnMed.Text = "Medium";
            //btnMed.Clicked += BtnMed_Clicked;
            //btnHard.Text = "Hard";
            //btnHard.Clicked += BtnHard_Clicked;

            stack.Children.Add(btnEasy);
            stack.Children.Add(btnMed);
            stack.Children.Add(btnHard);

            Content = stack;
        }

        //private void BtnHard_Clicked(object sender, EventArgs e)
        //{
        //    var gamepage = new GamePage();
        //    Navigation.PushAsync(new GamePage());
        //}

        //private void BtnMed_Clicked(object sender, EventArgs e)
        //{
        //    var gamepage = new GamePage();
        //    Navigation.PushAsync(new GamePage());
        //}

        //private void BtnEasy_Clicked(object sender, EventArgs e)
        //{
        //    var gamepage = new GamePage();
        //    Navigation.PushAsync(new GamePage());
        //}
    }
}