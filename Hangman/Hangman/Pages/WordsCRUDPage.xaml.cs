using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hangman.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordsCRUDPage : ContentPage
    {
        ListView WordListView;
        Entry UserInput;
        StackLayout MainstackLayout;
        int SelectedWordIndex = 0;
        Boolean isSelectedWord = false;
        List<int> words = new List<int>();
        public WordsCRUDPage()
        {
            InitializeComponent();

            MainstackLayout = new StackLayout();

            Label label = new Label();

            //List<string> list = new List<string>();

            //list = App.Database.GetRandomWordDBAsync().Result.Select(itm => itm.Word).ToList();

            //foreach (string i in list) 
            //{
            //    label.Text = i;
            //}

            /*
            if (App.Database.GetRandomWordDBAsync().Result.Word != null) 
            {
                label.Text = App.Database.GetRandomWordDBAsync().Result.Word;
            }
            */

            UserInput = new Entry
            {
                Text = "Enter in Word"
            };

            Button saveBTN = new Button
            {
                Text = "Save Word in DB"
            };

            Button deleteBTN = new Button
            {
                Text = "Delete Word from DB"
            };

            CreateListView();

            WordListView.ItemSelected += GetWordFromListView;

            saveBTN.Clicked += SaveWordDB;

            deleteBTN.Clicked += DeleteWordDB;

            MainstackLayout.Children.Add(label);

            MainstackLayout.Children.Add(UserInput);

            MainstackLayout.Children.Add(saveBTN);

            MainstackLayout.Children.Add(deleteBTN);

            Content = MainstackLayout;
        }
        async void CreateListView()
        {
            WordListView = new ListView
            {
                 ItemsSource = App.Database.GetWordsAsync().Result.Select(itm => itm.Word)
            };

            //Console.WriteLine("**************************");
            //Console.WriteLine(App.Database.GetWordsAsync().Result.Max(x => x.Id));
            //Console.WriteLine("**************************");

            //Random rand = new Random();
            //await App.Database.GetWordAsync(rand.Next(1, App.Database.GetWordsAsync().Result.Max(x => x.Id)));

            words = App.Database.GetWordsAsync().Result.Select(itm => itm.Id).ToList();

            MainstackLayout.Children.Add(WordListView);
        }

        async void SaveWordDB(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserInput.Text))
            {
                await App.Database.SaveWordAsync(new WordsModel
                {
                    Id = SelectedWordIndex,
                    Word = UserInput.Text
                });
                // SelectedWordIndex = 0;
                //UserInput.Text = string.Empty;
                //WordListView.ItemsSource = App.Database.GetWordsAsync().Result.Select(itm => itm.Word);
                 Navigation.PushAsync(new WordsCRUDPage()).Wait();
            }
        }
        async void DeleteWordDB(object sender, EventArgs e)
        {
            if (isSelectedWord != false)
            {
                WordsModel word = new WordsModel();
                word = await App.Database.GetWordAsync(SelectedWordIndex);

                if (word != null)
                {
                    await App.Database.DeleteWordAsync(word);
                    //UserInput.Text = string.Empty;
                    //WordListView.ItemsSource = App.Database.GetWordsAsync().Result.Select(itm => itm.Word);
                    Navigation.PushAsync(new WordsCRUDPage()).Wait();
                }
            }
        }

        async void GetWordFromListView(object sender, SelectedItemChangedEventArgs e)
        {
            var lvw = (ListView)sender;
            SelectedWordIndex = words[e.SelectedItemIndex];
            UserInput.Text = lvw.SelectedItem.ToString();
            Console.WriteLine("Id: " + SelectedWordIndex);
            isSelectedWord = true;
        }
    }
}