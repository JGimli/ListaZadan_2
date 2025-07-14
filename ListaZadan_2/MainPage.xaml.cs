using ListaZadan_2.Models;
using ListaZadan_2.ModelView;
using ListaZadan_2.Services;
using System.Threading.Tasks;

namespace ListaZadan_2
{
    public partial class MainPage : ContentPage
    {
        private SQLiteDB _context = new();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void SingInBmt(object sender, EventArgs e)
        {
            var viewModel = new ModelView.UserModelView(new Services.SQLiteDB());
            if (string.IsNullOrWhiteSpace(LoginEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Username and Password cannot be empty.", "OK");
                return;
            }
            viewModel.User.Name = LoginEntry.Text;
            viewModel.User.Password = PasswordEntry.Text;
            if (viewModel.LoginTaskAsync())
            {
                var services= new Services.SQLiteDB();
                var vm=new ModelView.UserTasksModelView(services);
                Navigation.PushAsync(new Tasks(vm));
            }
          
            }  

        private void RegisterPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void ForgotPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPage());
        }
    }

    }
