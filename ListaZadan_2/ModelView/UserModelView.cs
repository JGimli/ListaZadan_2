using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ListaZadan_2.Models;
using ListaZadan_2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaZadan_2.ModelView
{
    public partial class UserModelView : ObservableObject
    {
        private readonly SQLiteDB _context;

        public UserModelView(SQLiteDB context)
        {
            _context = context;
        }

        [ObservableProperty]
        private User _user = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _BusyText;



        public bool LoginTaskAsync() { 
            if (string.IsNullOrWhiteSpace(_user.Name) || string.IsNullOrWhiteSpace(_user.Password))
            {
                Shell.Current.DisplayAlert("Validation Error", "Username and Password cannot be empty.", "OK").Wait();
                return false;
            }
            
                var users = _context.GeFilterAsync<Models.User>(u => u.Name == _user.Name && u.Password == _user.Password);
                if (users != null)
                {
                    return true;
                }
                else
                {
                    Shell.Current.DisplayAlert("Login Failed", "Invalid username or password.", "OK").Wait();
                    return false;
                }
        }
        [RelayCommand]
        public async Task RegisterTaskAsync()
        {
            if (string.IsNullOrWhiteSpace(_user.Name) || string.IsNullOrWhiteSpace(_user.Password))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Username and Password cannot be empty.", "OK");
                return;
            }
            await ExecuteAsynch(async () =>
            {
                var users = await _context.GeFilterAsync<Models.User>(u => u.Name == _user.Name);
                if (users != null && users.Any())
                {
                    await Shell.Current.DisplayAlert("Registration Failed", "Username already exists.", "OK");
                }
                else
                {
                    await _context.AddItemAsynch(_user);
                    await Shell.Current.DisplayAlert("Registration Successful", "You can now log in.", "OK");
                    await Shell.Current.GoToAsync($"//MainPage");
                }
            });
        }
        [RelayCommand]
        private async Task RecowerTaskAsync()
        {
            if (string.IsNullOrWhiteSpace(_user.Name) || string.IsNullOrWhiteSpace(_user.Password))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Username and Password cannot be empty.", "OK");
                return;
            }
            await ExecuteAsynch(async ()=>
            {
                var users_data = await _context.GeFilterAsync<Models.User>(u => u.Name == _user.Name);
                if (users_data.Any())
                {
                    _context.UpdateItemAsynch<User>(_user);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Validation Error", "User dont exist.", "OK");
                    return;
                }
            });
        }
        private async Task ExecuteAsynch(Func<Task> operation, string busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Procesing";
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
                BusyText = "Procesing";
            }
        }
    }
}
