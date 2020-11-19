using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography.X509Certificates;


namespace Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HangManPage : ContentPage
    {
        Label[] label = new Label[5];
        public HangManPage()
        {
            InitializeComponent();

            label[0] = MyLabel1;
            label[1] = MyLabel2;
            label[2] = MyLabel3;
            label[3] = MyLabel4;
            label[4] = MyLabel5;

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
                    label[n].Text = Temp;
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