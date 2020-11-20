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
        Entry enterPlayerName;
        Entry enterUserName;
        Label bestScoreLbL;
        Label gemsLbL;

        ListView playerListView;
        int SelectedPlayerIndex = 0;
        Boolean isSelectedPlayer = false;
        List<int> players = new List<int>();

        StackLayout MainstackLayout;

        public ProfilePage()
        {
            InitializeComponent();

            MainstackLayout = new StackLayout() { Margin = new Thickness(20) };

            enterPlayerName = new Entry
            {
                Text = "Enter in player name"
            };

            enterUserName = new Entry
            {
                Text = "Enter in User Name"
            };

            bestScoreLbL = new Label
            {
                Text = "Score: 0",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            //bestScoreLbL.FormattedText = "Score: " + test;

            gemsLbL = new Label
            {
                Text = "Gems: 0",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            Button saveBTN = new Button
            {
                Text = "Create or Edit Player"
            };
            saveBTN.Clicked += SavePlayerDB;

            Button deleteBTN = new Button
            {
                Text = "Delete Player"
            };
            deleteBTN.Clicked += DeletePlayerDB;

            CreateListView();

            playerListView.ItemSelected += GetPlayerFromListView;

            MainstackLayout.Children.Add(enterPlayerName);

            MainstackLayout.Children.Add(enterUserName);

            MainstackLayout.Children.Add(bestScoreLbL);

            MainstackLayout.Children.Add(gemsLbL);

            MainstackLayout.Children.Add(saveBTN);

            MainstackLayout.Children.Add(deleteBTN);

            Content = MainstackLayout;
        }

        public void CreateListView()
        {

            players = App.Database.GetPlayersAsync().Result.Select(itm => itm.Id).ToList();


            playerListView = new ListView
            {
                ItemsSource = App.Database.GetPlayersAsync().Result
            };
            playerListView.ItemTemplate = new DataTemplate(typeof(PlayerCell));
            MainstackLayout.Children.Add(playerListView);
        }

        async void SavePlayerDB(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(enterPlayerName.Text))
            {
                await App.Database.SavePlayerAsync(new PlayerModel
                {
                    Id = SelectedPlayerIndex,
                    UserName = enterUserName.Text,
                    NameOfPlayer = enterPlayerName.Text
                });
                // SelectedWordIndex = 0;
                //UserInput.Text = string.Empty;
                //WordListView.ItemsSource = App.Database.GetWordsAsync().Result.Select(itm => itm.Word);
                await Navigation.PushAsync(new ProfilePage());
            }
        }
        async void DeletePlayerDB(object sender, EventArgs e)
        {
            if (isSelectedPlayer != false)
            {
                PlayerModel player = new PlayerModel();
                player = await App.Database.GetPlayerAsync(SelectedPlayerIndex);

                if (player != null)
                {
                    await App.Database.DeletePlayerAsync(player);
                    //UserInput.Text = string.Empty;
                    //WordListView.ItemsSource = App.Database.GetWordsAsync().Result.Select(itm => itm.Word);
                    await Navigation.PushAsync(new ProfilePage());
                }
            }
        }

        async void GetPlayerFromListView(object sender, SelectedItemChangedEventArgs e)
        {
            var lvw = (ListView)sender;
            SelectedPlayerIndex = players[e.SelectedItemIndex];

            PlayerModel player = new PlayerModel();
            player = await App.Database.GetPlayerAsync(SelectedPlayerIndex);


            enterPlayerName.Text = player.NameOfPlayer;
            enterUserName.Text = player.UserName;
            bestScoreLbL.FormattedText = "Best Score: " + player.BestScore.ToString();
            gemsLbL.FormattedText = "Gems: " + player.Gems.ToString();


            Console.WriteLine("Id: " + SelectedPlayerIndex);
            isSelectedPlayer = true;
        }



        /*
        public void SubmitProfile(object sender, EventArgs e)
        {
            var hangmanModel = new HangmanModel
            {
               // NameOfPlayer = enterPlayerName.Text
            };
            var gameDifficultyPage = new GameDifficultyPage();
            gameDifficultyPage.BindingContext = hangmanModel;
            //Navigation.PushAsync(gameDifficultyPage);
        }*/
    }
}