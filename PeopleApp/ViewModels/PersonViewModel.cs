using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PeopleApp.Models;
using Microsoft.Maui.Storage;  // Necesario para obtener la ruta correcta en .NET MAUI

namespace PeopleApp.Services
{
    public class DatabaseHelper
    {
        private SQLiteAsyncConnection _database;

        public DatabaseHelper()
        {
            try
            {
                // Usamos la ruta del directorio de la aplicación en dispositivos móviles
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "people.db3");

                // Crear la conexión a la base de datos SQLite
                _database = new SQLiteAsyncConnection(databasePath);
                // Crear la tabla si no existe
                _database.CreateTableAsync<Person>().Wait();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones en caso de que no se pueda abrir la base de datos
                Console.WriteLine($"Error al abrir la base de datos: {ex.Message}");
                throw;
            }
        }

        // Obtener lista de personas
        public Task<List<Person>> GetPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        // Guardar o actualizar persona
        public Task<int> SavePersonAsync(Person person)
        {
            if (person.Id != 0)
                return _database.UpdateAsync(person);  // Actualizar persona
            else
                return _database.InsertAsync(person);  // Insertar nueva persona
        }

        // Eliminar persona
        public Task<int> DeletePersonAsync(Person person)
        {
            return _database.DeleteAsync(person);  // Eliminar persona
        }
    }
}
