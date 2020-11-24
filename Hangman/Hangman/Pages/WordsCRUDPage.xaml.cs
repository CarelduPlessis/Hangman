using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hangman
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

        Button saveBTN;
        Button deleteBTN;

        string DefaultUserInput = "Enter in Word"; // Defualt Value for Entry: UserInput
        bool resetEntry = false;
        int countValidInput = 0;

        public WordsCRUDPage()
        {
            InitializeComponent();

            MainstackLayout = new StackLayout();

            Label label = new Label();

            UserInput = new Entry
            {
                Text = DefaultUserInput
            };
            UserInput.TextChanged += TextChanged;
            UserInput.Focused += OnFocus;
            UserInput.Unfocused += UnFocusEntry;

            saveBTN = new Button
            {
                Text = "Save Word in DB"
            };

            deleteBTN = new Button
            {
                Text = "Delete Word from DB"
            };
            deleteBTN.IsEnabled = false;

            CreateListView();

            WordListView.ItemSelected += GetWordFromListView;

            WordListView.ItemTapped += DeselectWord;

            saveBTN.Clicked += SaveWordDB;

            deleteBTN.Clicked += DeleteWordDB;

            MainstackLayout.Children.Add(label);

            MainstackLayout.Children.Add(UserInput);

            MainstackLayout.Children.Add(saveBTN);

            MainstackLayout.Children.Add(deleteBTN);

            Content = MainstackLayout;
        }
        public void CreateListView()
        {
            WordListView = new ListView
            {
                ItemsSource = App.Database.GetWordsAsync().Result.Select(itm => itm.Word),
                SelectionMode = (ListViewSelectionMode)SelectionMode.Single
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
            if (validation()) 
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
                    ReferenceThePage();
                }
            }
            else
            {
                await DisplayAlert("Invalid Entry", "You need to enter in a word that doesn't have more then " + _limit + " letters", "Ok");
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
                    deleteBTN.IsEnabled = false;
                    await App.Database.DeleteWordAsync(word);
                    //UserInput.Text = string.Empty;
                    //WordListView.ItemsSource = App.Database.GetWordsAsync().Result.Select(itm => itm.Word);
                    ReferenceThePage();
                }
            }
        }

        public void ReferenceThePage()
        {
            Navigation.PushAsync(new WordsCRUDPage());
        }

        async void GetWordFromListView(object sender, SelectedItemChangedEventArgs e)
        {
            if (isSelectedWord == false)
            {
                var lvw = (ListView)sender;
                SelectedWordIndex = words[e.SelectedItemIndex];
                WordsModel word = new WordsModel();
                word = await App.Database.GetWordAsync(SelectedWordIndex);


                UserInput.Text = word.Word;
                Console.WriteLine("Id: " + e.SelectedItemIndex);
                deleteBTN.IsEnabled = true;
                isSelectedWord = true;
            }
        }

        public void DeselectWord(object sender, ItemTappedEventArgs e)
        {
            if (isSelectedWord == true)
            {
                ((ListView)sender).SelectedItem = null;
                RestedALLEntrys();
                isSelectedWord = false;
                deleteBTN.IsEnabled = false;
            }
        }


        int _limit = 11;     //Enter text limit
        public void TextChanged(object sender, TextChangedEventArgs e)
        {
            //Website: Xamarin Forums
            //Title: Max length on Entry
            //URL: https://forums.xamarin.com/discussion/19285/max-length-on-entry
            var _entry = (Entry)sender;

            string _text = _entry.Text; //Get Current Text
            if (resetEntry == false)
            {
                if (_entry.Text.Any(ch => !Char.IsLetter(ch)))
                {
                    DisplayAlert("Invalid Input", "No Special Characters or Numbers", "OK");
                    RemoveCharacter(_text, sender);
                }

                if (_text.Length > _limit)       //If it is more than your character restriction
                {
                    DisplayAlert("Max Characters is " + _limit, "You can't have more than " + _limit + " Characters", "OK");
                    RemoveCharacter(_text, sender);
                }
            }
        }

        public void RemoveCharacter(string letter, object sender)
        {
            var _entry = (Entry)sender;
            letter = letter.Remove(letter.Length - 1); // Remove Last character
            _entry.Text = letter;        //Set the Old value
        }

        public void OnFocus(object sender, FocusEventArgs e)
        {
            var _entry = (Entry)sender;
            if (_entry.Text == DefaultUserInput)
            {
                _entry.Text = "";
            }
        }

        public void UnFocusEntry(object sender, EventArgs e)
        {
            var _entry = (Entry)sender;
            if (_entry.Text == "")
            {
                resetEntry = true;
                if (sender == UserInput)
                {
                    _entry.Text = DefaultUserInput;
                    resetEntry = false;
                }
            }
        }

        public bool validation()
        {
            if (UserInput.Text != DefaultUserInput && UserInput.Text != "")
            {
                countValidInput += 1;
            }

            if (countValidInput == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RestedALLEntrys()
        {
            resetEntry = true;
            UserInput.Text = DefaultUserInput;
            resetEntry = false;
        }
    }
}