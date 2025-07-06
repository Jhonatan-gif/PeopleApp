using PeopleApp.Models;
using PeopleApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PeopleApp.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        private readonly DatabaseHelper _databaseHelper;
        private string _name;
        private int _age;

        public ObservableCollection<Person> People { get; set; }
        public Person SelectedPerson { get; set; }
        public ICommand LoadPeopleCommand { get; }
        public ICommand AddNewCommand { get; }
        public string Message { get; set; }
        public bool IsMessageVisible { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public PersonViewModel()
        {
            _databaseHelper = new DatabaseHelper();
            People = new ObservableCollection<Person>();
            LoadPeopleCommand = new Command(async () => await LoadPeopleAsync());
            AddNewCommand = new Command(async () => await AddNewPersonAsync());
            Message = string.Empty;
            IsMessageVisible = false;
        }

        public async Task LoadPeopleAsync()
        {
            var peopleList = await _databaseHelper.GetPeopleAsync();
            People.Clear();
            foreach (var person in peopleList)
            {
                People.Add(person);
            }
        }

        public async Task AddNewPersonAsync()
        {
            if (!string.IsNullOrEmpty(Name) && Age > 0)
            {
                var newPerson = new Person { Name = Name, Age = Age };
                await _databaseHelper.SavePersonAsync(newPerson);
                await LoadPeopleAsync();

                Name = string.Empty;
                Age = 0;

                Message = "Persona agregada exitosamente";
                IsMessageVisible = true;

                await Task.Delay(3000);
                IsMessageVisible = false;
            }
            else
            {
                Message = "Por favor, ingrese un nombre y una edad válidos.";
                IsMessageVisible = true;
                await Task.Delay(3000);
                IsMessageVisible = false;
            }
        }
    }
}
