using System.Diagnostics;
using ToDoMauiClient.DataServices;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.Pages;

[QueryProperty(nameof(ToDo), "ToDo")]
public partial class ManageToDoPage : ContentPage
{
    private readonly IRestDataService _dataService;

	bool _isNew;
	ToDo _toDo;
	public ToDo toDo 
	{
		get => _toDo;
		set
		{
			_isNew = IsNew(value);
			_toDo= value;
			OnPropertyChanged();
        }
	}

	public ManageToDoPage(IRestDataService RestDataService)
	{
		InitializeComponent();
		_dataService = RestDataService;
		BindingContext = this;
	}

	bool IsNew(ToDo toDo)
	{
		if(toDo.Id == 0) 
			return true;
		return false;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if (_isNew)
		{
			Debug.WriteLine("ToDo item Added.");
            await _dataService.AddToDoAsync(toDo);
        }
        else
		{
            Debug.WriteLine("ToDo item Updated."); 
			await _dataService.UpdateToDoAsync(toDo);
        }
        await Shell.Current.GoToAsync("..");
    }

	async void OnDeleteButtonClicked(object sender,EventArgs e)
	{
		await _dataService.RemoveToDoAsync(toDo.Id);
		await Shell.Current.GoToAsync("..");
	}

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}