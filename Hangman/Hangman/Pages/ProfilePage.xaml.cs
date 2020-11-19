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

        public int gender = 1;
       
        public ProfilePage()
        {
            InitializeComponent();

            Image avatar = new Image
            {

                Source = "/Resources/drawable/Avatar" + 1 + ".jpg"
            };
            Entry PName = new Entry
            {
                Text = "",
                FontSize = 15,
                TextColor = Color.DarkBlue
            };
            Entry UName = new Entry
            {
                Text = "",
                FontSize = 15,
                TextColor = Color.DarkBlue
            };
            Label UScore = new Label
            {
                Text = "Score: 0",
                FontSize = 20,
                TextColor = Color.Orange
            };
            Label UGems = new Label
            {
                Text = "Gems: 0",
                FontSize = 20,
                TextColor = Color.Green
            };
            Button btnSave = new Button
            {
                Text = "Save",
                TextColor = Color.Blue
            };
            btnSave.Clicked += Save_Clicked;
            Button btnDelete = new Button
            {
                Text = "Delete",
                TextColor = Color.Red
            };
            btnDelete.Clicked += Delete_Clicked;

            Content = new StackLayout
            {
                Children =
            {
                avatar,
                new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        PName,
                        UName,
                        UScore,
                        UGems,
                        btnSave,
                        btnDelete,
                    }


                }

            }

            };

        }
        private void Save_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new SavePage());
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new DeletePage());
        }

    }
}