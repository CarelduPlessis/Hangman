using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Hangman
{
    public class HangmanModel : INotifyPropertyChanged
    {
        public HangmanModel()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NameOfPlayer { get; set; }
        public string Difficulty { get; set; }
        public string StateOfGame { get; set; }
        public int Score { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public PlayerModel childPlayerModel { get; set; }

        [ForeignKey(typeof(PlayerModel))]
        public int PlayerModelID { get; set; }
    }
}

