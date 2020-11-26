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
    public partial class LevelPage : ContentPage
    {
        public LevelPage()
        {
            InitializeComponent();

            Label Level = new Label
            {
                Text = "Here are levels to choose!",
                TextColor = Color.Blue,
                FontSize = 25
            };
            Button btnEasy = new Button
            {
                Text = "Easy",
                FontSize = 25,
                TextColor = Color.Green
            };
            btnEasy.Clicked += SelectDiff;

            Button btnMed = new Button
            {
                Text = "Medium",
                FontSize = 25,
                TextColor = Color.Yellow
            };
            btnMed.Clicked += SelectDiff;

            Button btnHard = new Button
            {
                Text = "Hard",
                FontSize = 25,
                TextColor = Color.Red
            };
            btnHard.Clicked += SelectDiff;
            
            Content = new StackLayout
            {
                Children =
                {
                    Level,
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            btnEasy,
                            btnMed,
                            btnHard,
                        }
                    }
                }
            };
        }
        public void SelectDiff(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var vm = BindingContext as HangmanModel;
            var hangmanModel = new HangmanModel
            {
                NameOfPlayer = vm.NameOfPlayer,
                Difficulty = btn.Text,
                PlayerModelID = vm.PlayerModelID
            };
            Console.WriteLine("************************");
            Console.WriteLine(vm.NameOfPlayer);
            Console.WriteLine("************************");
            /*
            var hangmanPage = new HangManPage();
            hangmanPage.BindingContext = hangmanModel;
            Navigation.PushAsync(hangmanPage);*/
        }   
    }
}