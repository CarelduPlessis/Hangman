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

        //Label[] label = new Label[25];
        public HangManPage()
        {
            InitializeComponent();

            Grid myGrid = new Grid();

            BoxView Box1 = new BoxView
            {
                Color = Color.Black

            };
            Grid.SetRow(myGrid, 0);

            Image picture = new Image
            {
                Source ="HME11.png"
            };
            Grid.SetRow(picture, 0);

            Label scoreLabel = new Label
            {
                Text = "Score: ",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Cyan
            };
            Grid.SetRow(scoreLabel, 0);

            BoxView letterBox = new BoxView
            {
                Color = Color.Black

            };
            Grid.SetRow(letterBox, 1);

            Label letterLabel1 = new Label
            {
                Text = "Hidden Wo_rd",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Cyan
            };
            Grid.SetRow(letterLabel1, 1);

            int letter = 65;
            //Value = A
            int letterZ = 90;
            char MyChar;
            
            //Rows
            for()


            myGrid.Children.Add(Box1);
            myGrid.Children.Add(picture);
            myGrid.Children.Add(scoreLabel);
            myGrid.Children.Add(letterBox);
            myGrid.Children.Add(letterLabel1);

            //label[0].Text = "A";
            //label[1].Text = "B";
            //label[2].Text = "C";
            //label[3].Text = "D";
            //label[4].Text = "E";
            //label[5].Text = "F";
            //label[6].Text = "G";
            //label[7].Text = "H";
            //label[8].Text = "I";
            //label[9].Text = "J";
            //label[10].Text = "K";
            //label[11].Text = "L";
            //label[12].Text = "M";
            //label[13].Text = "N";
            //label[14].Text = "O";
            //label[15].Text = "P";
            //label[16].Text = "Q";
            //label[17].Text = "R";
            //label[18].Text = "S";
            //label[19].Text = "T";
            //label[20].Text = "U";
            //label[21].Text = "V";
            //label[22].Text = "W";
            //label[23].Text = "X";
            //label[24].Text = "Y";
            //label[25].Text = "Z";

            //myGrid.Children.Add(label[0], 0, 0);
            //myGrid.Children.Add(label[1], 1, 0);
            //myGrid.Children.Add(label[2], 2, 0);
            //myGrid.Children.Add(label[3], 3, 0);
            //myGrid.Children.Add(label[4], 4, 0);
            //myGrid.Children.Add(label[5], 5, 0);
            //myGrid.Children.Add(label[6], 6, 0);
            //myGrid.Children.Add(label[7], 7, 0);
            //myGrid.Children.Add(label[8], 8, 1);
            //myGrid.Children.Add(label[9], 0, 1);
            //myGrid.Children.Add(label[10], 1, 1);
            //myGrid.Children.Add(label[11], 2, 1);
            //myGrid.Children.Add(label[12], 3, 1);
            //myGrid.Children.Add(label[13], 4, 1);
            //myGrid.Children.Add(label[14], 5, 1);
            //myGrid.Children.Add(label[15], 6, 1);
            //myGrid.Children.Add(label[16], 7, 1);
            //myGrid.Children.Add(label[17], 8, 1);
            //myGrid.Children.Add(label[18], 0, 2);
            //myGrid.Children.Add(label[19], 1, 2);
            //myGrid.Children.Add(label[20], 2, 2);
            //myGrid.Children.Add(label[21], 3, 2);
            //myGrid.Children.Add(label[22], 4, 2);
            //myGrid.Children.Add(label[23], 5, 2);
            //myGrid.Children.Add(label[24], 6, 2);
            //myGrid.Children.Add(label[25], 7, 2);

            Content = myGrid;


            //label[0] = MyLabel1;
            //label[1] = MyLabel2;
            //label[2] = MyLabel3;
            //label[3] = MyLabel4;
            //label[4] = MyLabel5;



        }


        //public ImageSource Source { get; set; }
        public string Temp;
        public string Word = "tiger";
        public int Times = 1;
        public void OnButtonClicked(object sender, EventArgs args)
        {
            char[] arr = Word.ToCharArray();
            for (int n = 0; n < Word.Length; n++)
            {
                Temp = ((Button)sender).Text;
                ((Button)sender).IsEnabled = false;
                if (Convert.ToChar(Temp) == arr[n])
                {
                    //label[n].Text = Temp;
                }
                else
                {
                    //Picture.Source = "/Resources/drawable/HME0" + Times + ".png";
                    Times = Times + 1;
                }
                Times = Times - n;

            }

        }

    }
}