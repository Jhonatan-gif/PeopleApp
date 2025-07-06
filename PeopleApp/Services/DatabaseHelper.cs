using SQLite;
using PeopleApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleApp.Services
{
    public class DatabaseHelper
    {
        private SQLiteAsyncConnection _database;

        public DatabaseHelper()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "people.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            return await _database.Table<Person>().ToListAsync();
        }

        public async Task SavePersonAsync(Person person)
        {
            if (person.Id != 0)
            {
                await _database.UpdateAsync(person);
            }
            else
            {
                await _database.InsertAsync(person);
            }
        }


        public async Task DeletePersonAsync(Person person)
        {
            await _database.DeleteAsync(person);
        }
    }
}
