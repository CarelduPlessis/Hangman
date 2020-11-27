using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SQLiteNetExtensionsAsync.Extensions;

namespace Hangman.Data
{
    public class HangmanDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public HangmanDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<PlayerModel>().Wait();
            _database.CreateTableAsync<HangmanModel>().Wait();
            _database.CreateTableAsync<WordsModel>().Wait();
        }

        public Task<List<PlayerModel>> GetPlayersAsync()
        {
            return _database.Table<PlayerModel>().ToListAsync();
        }

        public Task<PlayerModel> GetPlayerAsync(int id)
        {
            return _database.Table<PlayerModel>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SavePlayerAsync(PlayerModel player)
        {
            if (player.Id != 0)
            {
                return _database.UpdateAsync(player);
            }
            else
            {
                return _database.InsertAsync(player);
            }
        }

        public Task<int> DeletePlayerAsync(PlayerModel player)
        {
            return _database.DeleteAsync(player);
            //return _database.DeleteAllAsync<PlayerModel>();
        }

        public Task<List<HangmanModel>> GetHangmansAsync()
        {
            return _database.Table<HangmanModel>().ToListAsync();
        }

        public Task<HangmanModel> GetHangmanAsync(int id)
        {
            return _database.Table<HangmanModel>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveHangmanAsync(HangmanModel hangman)
        {
            if (hangman.Id != 0)
            {
                _database.UpdateAsync(hangman).Wait();
                if (hangman.childPlayerModel != null)
                {
                    _database.UpdateAsync(hangman);
                    return (Task<int>)_database.UpdateWithChildrenAsync(hangman);
                }
                else
                {
                    return _database.UpdateAsync(hangman);
                }

            }
            else
            {
                if (hangman.childPlayerModel != null)
                {
                    _database.InsertAsync(hangman);
                    return (Task<int>)_database.UpdateWithChildrenAsync(hangman);
                }
                else
                {
                    return _database.InsertAsync(hangman);
                }
            }
        }

        public Task<int> DeleteHangmanAsync(HangmanModel hangman)
        {
            return _database.DeleteAsync(hangman);
        }

        public Task<List<WordsModel>> GetWordsAsync()
        {
            return _database.Table<WordsModel>().ToListAsync();
        }

        public Task<WordsModel> GetWordAsync(int id)
        {
            return _database.Table<WordsModel>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveWordAsync(WordsModel word)
        {

            if (word.Id != 0)
            {
                return _database.UpdateAsync(word);
            }
            else
            {
                return _database.InsertAsync(word);
            }

        }

        //public Task<WordsModel> GetRandomWordDBAsync()
        //{
        //    var random = new Random();
        //    List<int> Ids = new List<int>();
        //    Ids = GetWordsAsync().Result.Select(itm => itm.Id).ToList();

        //    int index = random.Next(Ids.Count + 1);

        //    if (Ids.Count > 0)
        //    {
        //        Console.WriteLine("***************************************");
        //        Console.WriteLine(Ids.Count);
        //        Console.WriteLine(index);
        //        Console.WriteLine("***************************************");
        //        var word = GetWordAsync(Ids[index]);

        //        return word;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public Task<List<WordsModel>> GetRandomWordDBAsync()
        //{
        //    // Website name: MySQLTUTORIAL
        //    // Title: MySQL Select Random Records
        //    // URL: https://www.mysqltutorial.org/select-random-records-database-table.aspx
        //    return _database.QueryAsync<WordsModel>("SELECT Word FROM WordsModel ORDER BY random() LIMIT 1");
        //}

        /*
        public Task<List<WordsModel>> GetRandomWordDBAsync()
        {
            // Website name: MySQLTUTORIAL
            // Title: MySQL Select Random Records
            // URL: https://www.mysqltutorial.org/select-random-records-database-table.aspx
            //Random rnd = new Random();
            //rnd.Next();
            return _database.QueryAsync<WordsModel>("SELECT `Word` FROM `WordsModel` ORDER BY random() LIMIT 1");
        }
        */

        /*
        public Task<List<WordsModel>> GetRandomWordDBAsync()
        {
            // Website name: MySQLTUTORIAL
            // Title: MySQL Select Random Records
            // URL: https://www.mysqltutorial.org/select-random-records-database-table.aspx
            //https://www.sqlitetutorial.net/sqlite-functions/sqlite-random/
            return _database.QueryAsync<WordsModel>("SELECT Word FROM WordsModel ORDER BY random() LIMIT 1");
        }
        */


        //Website: Microsoft Documentation
        //Title: CA1828: Do not use CountAsync/LongCountAsync when AnyAsync can be used
        // URL: https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1828

        //Website: Microsoft Documentation
        //Title: Async return types (C#)
        // URL: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/async-return-types
        public async Task<string> CheckRandomID(int id)
        => await _database.Table<WordsModel>().Where(i => i.Id == id).CountAsync() != 0 ? "Not empty" : "Empty";

        public Task<int> DeleteWordAsync(WordsModel word)
        {
            return _database.DeleteAsync(word);
            //return _database.DeleteAllAsync<WordsModel>();
        }
    }
}
