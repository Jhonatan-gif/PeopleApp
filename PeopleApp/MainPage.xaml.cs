using PeopleApp.ViewModels;
using PeopleApp.Services;

namespace PeopleApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Crear una instancia de DatabaseHelper
            var databaseHelper = new DatabaseHelper();

            // Asigna el BindingContext con el ViewModel, pasando el DatabaseHelper
            BindingContext = new PersonViewModel(databaseHelper);
        }
    }
}
