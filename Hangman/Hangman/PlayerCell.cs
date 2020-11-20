using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hangman
{
    class PlayerCell : ViewCell
    {
        public PlayerCell()
        {
            //instantiate each of our views
            StackLayout cellWrapper = new StackLayout();
            StackLayout horizontalLayout = new StackLayout();
            Label userNameLBL = new Label();
            Label nameOfPlayerLBL = new Label();
            Label bestScoreLBL = new Label();
            Label gemsLBL = new Label();

            //set bindings
            userNameLBL.SetBinding(Label.TextProperty, "UserName");
            nameOfPlayerLBL.SetBinding(Label.TextProperty, "NameOfPlayer");
            bestScoreLBL.SetBinding(Label.TextProperty, "Gems");
            gemsLBL.SetBinding(Label.TextProperty, "BestScore");


            //Set properties for desired design
            cellWrapper.BackgroundColor = Color.FromHex("#eee");
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            nameOfPlayerLBL.HorizontalOptions = LayoutOptions.EndAndExpand;
            userNameLBL.TextColor = Color.FromHex("#f35e20");
            nameOfPlayerLBL.TextColor = Color.FromHex("503026");
            bestScoreLBL.TextColor = Color.FromHex("#f35e20");
            gemsLBL.TextColor = Color.FromHex("#f35e20");

            //add views to the view hierarchy
            horizontalLayout.Children.Add(userNameLBL);
            horizontalLayout.Children.Add(nameOfPlayerLBL);
            horizontalLayout.Children.Add(bestScoreLBL);
            horizontalLayout.Children.Add(gemsLBL);
            cellWrapper.Children.Add(horizontalLayout);
            View = cellWrapper;
        }
    }
}
