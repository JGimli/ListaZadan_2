using ListaZadan_2.Models;
using ListaZadan_2.Services;

namespace ListaZadan_2;

public partial class RegisterPage : ContentPage
{
	private SQLiteDB _context = new();
    public RegisterPage()
	{
		InitializeComponent();
	}

	private async void Sing_In_Btm(object sender, EventArgs e)
	{
		var viewModel = new ModelView.UserModelView(new Services.SQLiteDB());
		if (string.IsNullOrWhiteSpace(LoginEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
		{
			await DisplayAlert("Error", "Username and Password cannot be empty.", "OK");
			return;
        }
		viewModel.User.Name = LoginEntry.Text;
		viewModel.User.Password = PasswordEntry.Text;
		await viewModel.RegisterTaskAsync();
    }
}