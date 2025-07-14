using ListaZadan_2.ModelView;

namespace ListaZadan_2;

public partial class Tasks : ContentPage
{
	private readonly UserTasksModelView _viewModel;
    public Tasks(UserTasksModelView viewModel)
	{ 
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadTaskAsunc();
    }
}