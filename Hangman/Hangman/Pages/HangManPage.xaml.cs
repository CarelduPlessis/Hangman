using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography.X509Certificates;
using System.Windows;


namespace Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HangManPage : ContentPage
    {

        public HangManPage()
        {
            InitializeComponent();

            Grid myGrid = new Grid();

            BoxView scoreBox = new BoxView
            {
                Color = Color.Purple
            };
            Grid.SetRow(scoreBox, 0);
            Grid.SetColumnSpan(scoreBox, 7);

            int score = 0;
            Label scoreLabel = new Label
            {
                Text = "Score: " + Convert.ToString(score),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Cyan,
                FontSize = 30
            };
            Grid.SetRow(scoreLabel, 0);
            Grid.SetColumnSpan(scoreLabel, 7);

            BoxView imageBox = new BoxView
            {
                Color = Color.Black
            };
            Grid.SetRow(imageBox, 1);
            Grid.SetRowSpan(imageBox, 3);
            Grid.SetColumnSpan(imageBox, 7);

            int HMpicture = 1;
            Image HMimage = new Image
            {
                Source = "HM0" + Convert.ToString(HMpicture) + ".png",
            };
            Grid.SetRow(HMimage, 1);
            Grid.SetRowSpan(HMimage, 3);
            Grid.SetColumnSpan(HMimage, 7);

            BoxView letterBox = new BoxView
            {
                Color = Color.Purple
            };
            Grid.SetRow(letterBox, 4);
            Grid.SetColumnSpan(letterBox, 7);

            string word = "_";
            Label letterLabel = new Label
            {
                Text = word,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Cyan,
                FontSize = 30
            };
            Grid.SetRow(letterLabel, 4);
            Grid.SetColumnSpan(letterLabel, 7);

            int letter = 65;
            //Value = A
            int letterZ = 90;
            char MyChar;

            //Rows
            for (int r = 5; letter <= letterZ; r++)
            {
                //Columns
                for (int c = 0; c < 7 && letter <= letterZ; c++)
                {
                    MyChar = Convert.ToChar(letter);
                    //myGrid.Children.Add(new BoxView
                    //{
                    //    Margin = 0,
                    //    HeightRequest = 40
                    //}, c, r);
                    myGrid.Children.Add(new Button
                    {
                        Text = Convert.ToString(MyChar),
                        FontSize = 20,
                    }, c, r);
                    letter++;

                }
            }
            //MyChar.Clicked += OnButtonClicked;

            myGrid.Children.Add(scoreBox);
            myGrid.Children.Add(scoreLabel);
            myGrid.Children.Add(imageBox);
            myGrid.Children.Add(HMimage);
            myGrid.Children.Add(letterBox);
            myGrid.Children.Add(letterLabel);

            Content = myGrid;

        }
    }
}