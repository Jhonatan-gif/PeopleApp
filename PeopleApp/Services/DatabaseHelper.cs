using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PeopleApp.Models;
using PeopleApp.Services;

namespace PeopleApp.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        private readonly DatabaseHelper _databaseHelper;

        public ObservableCollection<Person> People { get; set; }
        public Person SelectedPerson { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddNewCommand { get; }

        public PersonViewModel(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
            People = new ObservableCollection<Person>();

            // Cargar las personas al inicio
            LoadPeople();

            SaveCommand = new Command(SavePerson);
            DeleteCommand = new Command(DeletePerson);
            AddNewCommand = new Command(AddNewPerson);
        }

        // Cargar las personas desde la base de datos
        private async void LoadPeople()
        {
            var peopleList = await _databaseHelper.GetPeopleAsync();
            People.Clear();
            foreach (var person in peopleList)
            {
                People.Add(person);
            }
        }

        // Guardar persona (si es nueva o actualizar si existe)
        private async void SavePerson()
        {
            if (SelectedPerson != null)
            {
                await _databaseHelper.SavePersonAsync(SelectedPerson);
                LoadPeople();
            }
        }

        // Eliminar persona
        private async void DeletePerson()
        {
            if (SelectedPerson != null)
            {
                await _databaseHelper.DeletePersonAsync(SelectedPerson);
                LoadPeople();
            }
        }

        // Agregar una nueva persona
        private void AddNewPerson()
        {
            SelectedPerson = new Person();  // Crea una nueva persona vacía
        }
    }
}
