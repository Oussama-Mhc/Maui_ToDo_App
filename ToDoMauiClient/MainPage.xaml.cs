using System.Diagnostics;
using ToDoMauiClient.DataServices;
using ToDoMauiClient.Models;
using ToDoMauiClient.Pages;

namespace ToDoMauiClient;

public partial class MainPage : ContentPage
{
    private readonly IRestDataService _dataservice;

    public MainPage(IRestDataService dataservice)
	{
		InitializeComponent();
		_dataservice = dataservice;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        collectionView.ItemsSource = await _dataservice.GetAllToDosAsync();  
    }

    async void OnAddToDoClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("-----> Add button clicked");
        var navigationProperty = new Dictionary<string, object>
        {
            { nameof(ToDo), new ToDo() }
        };

        await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationProperty);
    }
    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine("-----> Item changed clicked");

        var navigationProperty = new Dictionary<string, object>
        {
            { nameof(ToDo), e.CurrentSelection.FirstOrDefault() as ToDo }
        };

        await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationProperty);
    }

}

