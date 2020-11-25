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
    public partial class TestRandomWordPage : ContentPage
    {
        Label wordLBL;
        Button nextWordBTN;
        Random randomWord = new Random();
        //List<WordsModel> word = new List<WordsModel>();
        WordsModel word = new WordsModel();
        int RandID = 0;
        public TestRandomWordPage()
        {
            InitializeComponent();

            StackLayout STLayout = new StackLayout();

            wordLBL = new Label() 
            { 
                Text = "Random Words"
            };

            nextWordBTN = new Button() 
            { 
                Text = "Next Random Word"
            };
            nextWordBTN.Clicked += NextWord;

            STLayout.Children.Add(wordLBL);
            STLayout.Children.Add(nextWordBTN);
            Content = STLayout;
        }

        async void NextWord(object sender, EventArgs e) 
        {
            var condition = "Empty";
            while (condition == "Empty") 
            {
                RandID = randomWord.Next(1, App.Database.GetWordsAsync().Result.Max(x => x.Id) + 1);
                condition = $"{await App.Database.CheckRandomID(RandID)}";
            }
            word = App.Database.GetWordAsync(RandID).Result;
            wordLBL.Text = word.Word;
        }
    }
}