using PeopleApp.ViewModels;

namespace PeopleApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new PersonViewModel();
    }
}

