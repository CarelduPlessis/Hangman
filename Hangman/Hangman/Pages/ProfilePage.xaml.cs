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
    public partial class ProfilePage : ContentPage
    {
        // Elements For CRUD operations
        Entry entryPlayerName;
        Entry entryUserName;
        Entry entryPlayerGender;
        Label bestScoreLbL;
        Label gemsLbL;
        Button saveBTN;
        Button deleteBTN;

        // Select Player Profile for your Hangman game
        Button selectPlayerBTN;

        //PlayerListViews Variables (For Logic)
        ListView playerListView;
        int SelectedPlayerIndex = 0; // Get DB ID of the Selected Player form ListView
        Boolean isSelectedPlayer = false; // Keep Track When the Player is selected (in order to Deselect/ select)
        List<int> players = new List<int>(); // store ids from DB

        // Headers For the PlayerListView 
        Label hdNameLabel = new Label { Text = "UserName", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)), VerticalTextAlignment = TextAlignment.Center, HeightRequest = 20 };
        Label hdLastNameLabel = new Label { Text = "Player", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, HeightRequest = 20 };
        Label hdBestScoreLabel = new Label { Text = "BestScore", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, HeightRequest = 20 };
        Label hdGemsLabel = new Label { Text = "Gems", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, HeightRequest = 20 };
        Label hdAvatarLabel = new Label { Text = "Avatar", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, HeightRequest = 20 };

        //Layout 
        StackLayout MainstackLayout;
        Grid ListViewHeader;
        Grid ListViewGrid;

        // Place Holders and Default Values
        string DefaultPlayerName = "Enter in player name"; // Defualt Value for Entry: player name
        string DefaultUserName = "Enter in User Name"; // Defualt Value for Entry: User Name
        string DefaultPlayerGender = "Player Gender: Female or Male"; // Defualt Value for Entry: Player Gender

        bool resetEntry = false; // Track When Reseting Entry to void clashing with validation
        string ImageName; // keep track of the Images (Avatar Images)

        int countValidInput = 0; // Check Validation before saving data to DB
        
        #region ProfilePage()
        public ProfilePage()
        {
            InitializeComponent();

            #region Layouts
            MainstackLayout = new StackLayout(); // main Layout 

            ListViewHeader = new Grid(); // Grid for Header for listview 

            ListViewGrid = new Grid(); // Grid for ListView 
            Grid.SetRow(ListViewGrid, 1);
            #endregion

            #region Entry, Label and Buttons
            entryPlayerName = new Entry
            {
                Text = DefaultPlayerName
            };
            entryPlayerName.TextChanged += TextChanged;
            entryPlayerName.Focused += OnFocus;
            entryPlayerName.Unfocused += UnFocusEntry;

            entryUserName = new Entry
            {
                Text = DefaultUserName
            };
            entryUserName.TextChanged += TextChanged;
            entryUserName.Focused += OnFocus;
            entryUserName.Unfocused += UnFocusEntry;

            entryPlayerGender = new Entry
            {
                Text = DefaultPlayerGender
            };
            entryPlayerGender.TextChanged += TextChanged;
            entryPlayerGender.Focused += OnFocus;
            entryPlayerGender.Unfocused += UnFocusEntry;

            bestScoreLbL = new Label
            {
                Text = "Score: 0",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            gemsLbL = new Label
            {
                Text = "Gems: 0",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            saveBTN = new Button
            {
                Text = "Create or Edit Player"
            };
            saveBTN.Clicked += SavePlayerDB;

            deleteBTN = new Button
            {
                Text = "Delete Player"
            };
            deleteBTN.Clicked += DeletePlayerDB;
            deleteBTN.IsEnabled = false;

            selectPlayerBTN = new Button
            {
                Text = "Select Player"
            };
            selectPlayerBTN.IsEnabled = false;
            selectPlayerBTN.Clicked += SelectProfile;
            #endregion

            #region Add Elements to Main Layout
            MainstackLayout.Children.Add(selectPlayerBTN);

            // the Headers position on the screen
            ListViewHeader.Children.Add(hdNameLabel);
            ListViewHeader.Children.Add(hdLastNameLabel, 1, 0);
            ListViewHeader.Children.Add(hdBestScoreLabel, 2, 0);
            ListViewHeader.Children.Add(hdGemsLabel, 3, 0);
            ListViewHeader.Children.Add(hdAvatarLabel, 4, 0);

            MainstackLayout.Children.Add(ListViewHeader);

            CreateListView(); // Create listview 

            ListViewGrid.Children.Add(playerListView); // Add Listview to ListViewGrid

            MainstackLayout.Children.Add(ListViewGrid);

            playerListView.ItemSelected += GetPlayerFromListView;

            playerListView.ItemTapped += DeselectPlayer;

            MainstackLayout.Children.Add(entryPlayerName);

            MainstackLayout.Children.Add(entryUserName);

            MainstackLayout.Children.Add(entryPlayerGender);

            MainstackLayout.Children.Add(bestScoreLbL);

            MainstackLayout.Children.Add(gemsLbL);

            MainstackLayout.Children.Add(saveBTN);

            MainstackLayout.Children.Add(deleteBTN);
            #endregion

            // Add MainLayout to ScrollView 
            ScrollView scrollView = new ScrollView { Content = MainstackLayout };

            // Assigning ScrollView To Content
            Content = scrollView;
        }
        #endregion

        #region Create the ListView
        public void CreateListView()
        {
            // Store All the ids in a List
            var items = App.Database.GetPlayersAsync().Result;
            players = items.Select(itm => itm.Id).ToList();
            //var usernames = items.Find(x => x.Id == 1).UserName;
            // Creat listview
            playerListView = new ListView
            {
                ItemsSource = items,
                Margin = new Thickness(0, 20, 0, 0),
                HeightRequest = Application.Current.MainPage.Width * 0.5,
                SelectionMode = (ListViewSelectionMode)SelectionMode.Single
            };

            // Add Custom listview Template (custom layout of listview)
            playerListView.ItemTemplate = new DataTemplate(typeof(PlayerCell));
        }
        #endregion

        #region Save Player To DataBase
        async void SavePlayerDB(object sender, EventArgs e)
        {
            if (validation()) // if input is valid then
            {
                // Save input to database
                await App.Database.SavePlayerAsync(new PlayerModel
                {
                   Id = SelectedPlayerIndex,
                   UserName = entryUserName.Text,
                   NameOfPlayer = entryPlayerName.Text,
                   AvatarOfPlayer = ImageName
                });
                RefreshThePage(); // refresh the page (avoid error)
            }
            else // Display Alert Message
            {
                await DisplayAlert("Invalid Entry", "Enter in a Username, Player Name and Choose a Gender: Male or Female", "Ok");
            }
        }
        #endregion

        #region Delete Player From DataBase 
        async void DeletePlayerDB(object sender, EventArgs e)
        {
            // Get player to be deleted 
            PlayerModel player = new PlayerModel();
            player = await App.Database.GetPlayerAsync(SelectedPlayerIndex);

            if (player != null) // if player exist then 
            {
                deleteBTN.IsEnabled = false; // disable delete Button 
                await App.Database.DeletePlayerAsync(player); // Delete Player for DB
                RefreshThePage(); // refresh the page (avoid error)
            }
        }
        #endregion

        #region Refresh The Page
        public void RefreshThePage()
        {
            // Go to this page to refresh the page (helps to avoid errors)
            Navigation.PushAsync(new ProfilePage());
        }
        #endregion

        #region Find Selected Player From The ListView
        async void GetPlayerFromListView(object sender, SelectedItemChangedEventArgs e)
        {
            if (isSelectedPlayer == false) // if player is not Selected then
            {
                // Find player in DB
                SelectedPlayerIndex = players[e.SelectedItemIndex];
                PlayerModel player = new PlayerModel();
                player = await App.Database.GetPlayerAsync(SelectedPlayerIndex);

               //player = App.Database.GetPlayerAsync(Convert.ToInt32(e.SelectedItem)).Result;

                //Display the Player information
                entryPlayerName.Text = player.NameOfPlayer;
                entryUserName.Text = player.UserName;
                bestScoreLbL.FormattedText = "Best Score: " + player.BestScore.ToString();
                gemsLbL.FormattedText = "Gems: " + player.Gems.ToString();

                // Display Gender in Label
                if (player.AvatarOfPlayer == "Avatar2.jpg") // Work Out Gender Based on image
                {
                    entryPlayerGender.Text = "female";
                }
                else if (player.AvatarOfPlayer == "Avatar1.jpg")
                {
                    entryPlayerGender.Text = "male";
                }

               // Console.WriteLine("DB Id: " + SelectedPlayerIndex); // Testing purposes

                deleteBTN.IsEnabled = true; // enable delete button
                isSelectedPlayer = true; // set player as selected 
                selectPlayerBTN.IsEnabled = true; // enable Select Player button
            }
        }
        #endregion

        #region Deselect The Player From ListView
        public void DeselectPlayer(object sender, ItemTappedEventArgs e)
        {
            if (isSelectedPlayer == true) // if player is selected then 
            {
                // Deselect Player
                ((ListView)sender).SelectedItem = null;
                SelectedPlayerIndex = 0;
                RestedALLEntrys(); // reste all the input controls to defualt values 
                isSelectedPlayer = false; // no player is selected 
                deleteBTN.IsEnabled = false; // disable delete button
                selectPlayerBTN.IsEnabled = false; // disable Select Player button
            }
        }
        #endregion

        #region When the Text Change, Start Validating
        int _limit = 10;     //Enter text limit
        public void TextChanged(object sender, TextChangedEventArgs e)
        {
            //Website: Xamarin Forums
            //Title: Max length on Entry
            //URL: https://forums.xamarin.com/discussion/19285/max-length-on-entry

            var _entry = (Entry)sender; // Get Current Entry Object 

            string _text = _entry.Text; //Get Current Text

            if (resetEntry == false) // if no entrys are being reset then
            {
                //Start Check validation 
                if (_entry.Text.Any(ch => !Char.IsLetterOrDigit(ch))) // only accepts Letter or Numbers
                {
                    DisplayAlert("Invalid Input", "No Special Characters", "OK");
                    RemoveCharacter(_text, sender);
                }

                if (_text.Length > _limit) // If it is more than your character restriction
                {
                    DisplayAlert("Max Characters is " + _limit, "You can't have more than " + _limit + " Characters", "OK");
                    RemoveCharacter(_text, sender);
                }
            }
            
            if (sender == entryPlayerGender) // the Gender entry only accepts female or male as valide input
            {
                if (_text.ToLower() == "female")
                {
                    ImageName = "Avatar2.jpg";
                }
                else if (_text.ToLower() == "male")
                {
                    ImageName = "Avatar1.jpg";
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
            // Checks if entry has it Default value if so then remove default text form entry
            if (_entry.Text == DefaultPlayerName || _entry.Text == DefaultUserName || _entry.Text == DefaultPlayerGender)
            {
                _entry.Text = "";
            }
        }
        #endregion

        #region When the Entry is UnFocus (Give Entry its Default Text if Entry is Empty)
        public void UnFocusEntry(object sender, EventArgs e)
        {
            // Entry: on leaving focus then 
            var _entry = (Entry)sender; // get current entry 
            if (_entry.Text == "") // if entry text is empty then 
            {
                resetEntry = true; // turn off validation 

                // find entry and add their defualt text
                if (sender == entryPlayerName) 
                {
                    _entry.Text = DefaultPlayerName;
                    resetEntry = false;  // turn on validation 
                }
                else if (sender == entryUserName)
                {
                    _entry.Text = DefaultUserName;
                    resetEntry = false; // turn on validation 
                }
                else if (sender == entryPlayerGender)
                {
                    _entry.Text = DefaultPlayerGender;
                    resetEntry = false;  // turn on validation 
                }
            }
        }
        #endregion

        #region Check Validation Before Saving Player To Database
        /*
            Check input if it is valide before saving input
        */
        public bool validation()
        {
            // check if Gender entry input is valide: input is restricted to female or male
            if (entryPlayerGender.Text.ToLower() == "female" || entryPlayerGender.Text.ToLower() == "male")
            {
                countValidInput += 1;
            }

            // check if entry is doesn't contain default text
            if (entryUserName.Text != DefaultUserName)
            {
                countValidInput += 1;
            }

            // check if entry is doesn't contain default text
            if (entryPlayerName.Text != DefaultPlayerName)
            {
                countValidInput += 1;
            }

            if (countValidInput == 3) // check if all input is valid
            {
                return true;
            }
            else
            {  
                countValidInput = 0;
                return false;
            }
        }
        #endregion

        #region Rest ALL Entrys
        public void RestedALLEntrys()
        {
            resetEntry = true;
            entryPlayerName.Text = DefaultPlayerName;
            entryUserName.Text = DefaultUserName;
            entryPlayerGender.Text = DefaultPlayerGender;
            resetEntry = false;
        }
        #endregion

        #region Select Profile to use in New Game of Hangman
        /*
         * Select the player Profile for the current game
        */
        public void SelectProfile(object sender, EventArgs e)
        {
            //var hangmanModel = new HangmanModel
            //{
            //    NameOfPlayer = entryPlayerName.Text,
            //    PlayerModelID = SelectedPlayerIndex
            //};
            // Go to next page with the data
            //var levelPage = new LevelPage();
            //levelPage.BindingContext = hangmanModel;
            //Navigation.PushAsync(levelPage);
        }
        #endregion
    }
}