// Yapilacaklar.xaml.cs
using FİNALODEV.MODEL;
using System.Collections.ObjectModel;
using System.Linq;

namespace FİNALODEV
{
    public partial class Yapilacaklar : ContentPage
    {
        string message = "";
        public Yapilacaklar()
        {
            InitializeComponent();
            LoadTasks();
            listTask.ItemsSource = BL.Tasks;
        }

        private async void LoadTasks()
        {
            try
            {
                var result = await BL.LoadTasks();
                if (!result.success)
                    await DisplayAlert("Error", result.message, "Cancel");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private async void TaskEkleEvent(object sender, EventArgs e)
        {
            try
            {
                var page = new yapilacaklarDetay()
                {
                    Title = "Görev Ekle"
                };
                page.AddMethod = async (task) =>
                {
                    var yapilacak = new MODEL.Yapilacaklar
                    {
                        Title = task.Title,
                        Description = task.Note,
                        CreatedDate = task.TaskDateTime
                    };
                    await AddTask(yapilacak);
                };
                await Navigation.PushModalAsync(page);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private async void TaskDuzenleEvent(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as MenuFlyoutItem;
                var task = BL.Tasks.FirstOrDefault(o => o.Id == menuItem.CommandParameter.ToString());
                if (task == null) return;

                var myTask = new MyTask
                {
                    Title = task.Title,
                    Note = task.Description,
                    TaskDateTime = task.CreatedDate
                };

                var page = new yapilacaklarDetay(myTask)
                {
                    Title = "Görev Düzenle"
                };
                page.AddMethod = async (updatedTask) =>
                {
                    task.Title = updatedTask.Title;
                    task.Description = updatedTask.Note;
                    task.CreatedDate = updatedTask.TaskDateTime;
                    await EditTask(task);
                };
                await Navigation.PushModalAsync(page);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private async void TaskSilEvent(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as MenuFlyoutItem;
                var task = BL.Tasks.First(o => o.Id == menuItem.CommandParameter.ToString());
                bool answer = await DisplayAlert("Silmeyi Onayla", $"{task.Title} silinsin mi?", "Evet", "Hayır");
                if (answer)
                {
                    var result = await BL.DeleteTask(task);
                    if (!result.success)
                        await DisplayAlert("Error", result.message, "Cancel");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private async Task AddTask(MODEL.Yapilacaklar task)
        {
            try
            {
                var result = await BL.AddTask(task);
                if (!result.success)
                    await DisplayAlert("Error", result.message, "Cancel");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private async Task EditTask(MODEL.Yapilacaklar task)
        {
            try
            {
                var result = await BL.EditTask(task);
                if (!result.success)
                    await DisplayAlert("Error", result.message, "Cancel");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                BL.Tasks.Clear();
                LoadTasks();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Cancel");
            }
        }
    }
}