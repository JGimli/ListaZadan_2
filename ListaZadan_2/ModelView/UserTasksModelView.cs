using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ListaZadan_2.Models;
using ListaZadan_2.Services;
using System.Collections.ObjectModel;

namespace ListaZadan_2.ModelView;

public partial class UserTasksModelView : ObservableObject
{
    private readonly SQLiteDB _context;

    public UserTasksModelView(SQLiteDB context)
    {
        _context = context;
    }
    [ObservableProperty]
    private ObservableCollection<UserTasks> _userTasks = new();

    [ObservableProperty]
    private UserTasks _selectedUserTask = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _BusyText;

    public async Task LoadTaskAsunc()
    {

        await ExecuteAsynch(async () =>
        {
            var userTasks = await _context.GetItemsAsync<UserTasks>();
            if (userTasks != null && userTasks.Any())
            {
                UserTasks ??= new ObservableCollection<UserTasks>();
                foreach (var task in userTasks)
                {
                    UserTasks.Add(task);
                }
            }
        }, "Loading Tasks");


    }

    [RelayCommand]
    private void SetSelectedTask(UserTasks? task) => SelectedUserTask = task ?? new();

    [RelayCommand]
    private async Task SaveTaskAsync()
    {
        if (SelectedUserTask is null)
            return;

        var (isValid, errorMessage) = SelectedUserTask.Validate();
        if (!isValid) { 
            await Shell.Current.DisplayAlert("Validation Error", errorMessage, "OK");
            return;
        }

        await ExecuteAsynch(async () =>
        {
            if (SelectedUserTask.Id == 0)
            {
                await _context.AddItemAsynch<UserTasks>(SelectedUserTask);
                UserTasks.Add(SelectedUserTask);
            }
            else
            {
                await _context.UpdateItemAsynch<UserTasks>(SelectedUserTask);
                var taskCompy = SelectedUserTask.Clone();
                var index = UserTasks.IndexOf(SelectedUserTask);
                UserTasks.RemoveAt(index);
                UserTasks.Insert(index, taskCompy);
            }
            setSelectedTaskCommand.Execute(new());
        }, "Saving Task");
    }
    [RelayCommand]
    private async Task DeleteTaskAsync(int id)
    {
        await ExecuteAsynch(async () =>
        {
            if (await _context.DeleteItemByKeyAsynch<UserTasks>(id))
            {
                var task = UserTasks.FirstOrDefault(x => x.Id == id);
                UserTasks.Remove(task);
            }
            else
            {
                await Shell.Current.DisplayAlert("Delete Error", "Task could not be deleted", "OK");
            }
        }, "Deleting Task");


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