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
        Entry UserInput; // Enter in Words
        Button saveBTN; // button to Save word to DB
        Button deleteBTN; // button to delete word from DB

        //ListView Variables 
        ListView WordListView;  // Display all words in DB
        int SelectedWordIndex = 0; // Get DB ID of the Selected word form ListView
        Boolean isSelectedWord = false; // Keep Track When the word is selected (in order to Deselect/ select)
        List<int> words = new List<int>(); // store ids from DB


        string DefaultUserInput = "Enter in Word"; // Defualt Value for Entry: UserInput
        bool resetEntry = false; // Track When Reseting Entry to void clashing with validation
        int countValidInput = 0; // Check Valid Input

        // Layouts 
        StackLayout MainstackLayout;

        public WordsCRUDPage()
        {
            InitializeComponent();

            MainstackLayout = new StackLayout();

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
            saveBTN.Clicked += SaveWordDB;

            deleteBTN = new Button
            {
                Text = "Delete Word from DB"
            };
            deleteBTN.IsEnabled = false;
            deleteBTN.Clicked += DeleteWordDB;

            CreateListView();

            WordListView.ItemSelected += GetWordFromListView;

            WordListView.ItemTapped += DeselectWord;

            MainstackLayout.Children.Add(UserInput);

            MainstackLayout.Children.Add(saveBTN);

            MainstackLayout.Children.Add(deleteBTN);

            Content = MainstackLayout;
        }

        #region Create the ListView
        public void CreateListView()
        {
            //creat the ListView
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

            // Store all the ids to a List
            words = App.Database.GetWordsAsync().Result.Select(itm => itm.Id).ToList();

            MainstackLayout.Children.Add(WordListView);
        }
        #endregion 

        #region Save Word To DataBase  
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
                    RefreshThePage();
                }
            }
            else
            {
                await DisplayAlert("Invalid Entry", "You need to enter in a word that doesn't have more then " + _limit + " letters", "Ok");
            }
        }
        #endregion

        #region Delete Word From DataBase   
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
                    RefreshThePage();
                }
            }
        }
        #endregion

        #region Refresh The Page
        public void RefreshThePage()
        {
            Navigation.PushAsync(new WordsCRUDPage());
        }
        #endregion

        #region Get the selected word from the ListView
        async void GetWordFromListView(object sender, SelectedItemChangedEventArgs e)
        {
            if (isSelectedWord == false)
            {
                var lvw = (ListView)sender;
                SelectedWordIndex = words[e.SelectedItemIndex];
                WordsModel word = new WordsModel();
                word = await App.Database.GetWordAsync(SelectedWordIndex);

                UserInput.Text = word.Word;
                Console.WriteLine("List Id: " + e.SelectedItemIndex);
                Console.WriteLine("DB Id: " + SelectedWordIndex);
                deleteBTN.IsEnabled = true;
                isSelectedWord = true;
            }
        }
        #endregion

        #region Deselect the Word from ListView
        public void DeselectWord(object sender, ItemTappedEventArgs e)
        {
            if (isSelectedWord == true)
            {
                ((ListView)sender).SelectedItem = null;
                RestedALLEntrys();
                isSelectedWord = false;
                deleteBTN.IsEnabled = false;
                SelectedWordIndex = 0;
            }
        }
        #endregion

        #region When the Text Change, Start Validating
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
        #endregion

        #region Remove Character (Invalid Characters)
        public void RemoveCharacter(string letter, object sender)
        {
            var _entry = (Entry)sender;
            letter = letter.Remove(letter.Length - 1); // Remove Last character
            _entry.Text = letter;        //Set the Old value
        }
        #endregion

        #region When the Entry is OnFocus (Remove Default Text)
        public void OnFocus(object sender, FocusEventArgs e)
        {
            var _entry = (Entry)sender;
            if (_entry.Text == DefaultUserInput)
            {
                _entry.Text = "";
            }
        }
        #endregion

        #region When the Entry is UnFocus (Give Entry its Default Text if Entry is Empty)
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
        #endregion

        #region Check Validation before Saving Word To Database
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
        #endregion

        #region Rest ALL Entrys
        public void RestedALLEntrys()
        {
            resetEntry = true;
            UserInput.Text = DefaultUserInput;
            resetEntry = false;
        }
        #endregion
    }
}