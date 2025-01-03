// yapilacaklarDetay.xaml.cs
namespace FİNALODEV
{
    public class MyTask
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime TaskDateTime { get; set; }
    }

    public partial class yapilacaklarDetay : ContentPage
    {
        public bool Result = false;
        public MyTask mTask = null;
        bool edit = false;
        public Action<MyTask> AddMethod = null;

        public yapilacaklarDetay(MyTask task = null)
        {
            InitializeComponent();

            if (task == null)
            {
                mTask = new MyTask
                {
                    TaskDateTime = DateTime.Now
                };
                datePicker.Date = DateTime.Now.Date;
                timePicker.Time = DateTime.Now.TimeOfDay;
                edit = false;
            }
            else
            {
                mTask = task;
                txttitle.Text = task.Title;
                txtnote.Text = task.Note;
                datePicker.Date = task.TaskDateTime.Date;
                timePicker.Time = task.TaskDateTime.TimeOfDay;
                edit = true;
            }
        }

        private void BugunClicked(object sender, EventArgs e)
        {
            datePicker.Date = DateTime.Now.Date;
        }

        private void SimdiClicked(object sender, EventArgs e)
        {
            timePicker.Time = DateTime.Now.TimeOfDay;
        }

        private async void OKClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txttitle.Text))
                {
                    await DisplayAlert("Uyarı", "Lütfen bir görev başlığı girin.", "Tamam");
                    return;
                }

                Result = true;
                mTask.Title = txttitle.Text;
                mTask.Note = txtnote.Text;
                mTask.TaskDateTime = new DateTime(
                    datePicker.Date.Year,
                    datePicker.Date.Month,
                    datePicker.Date.Day,
                    timePicker.Time.Hours,
                    timePicker.Time.Minutes,
                    0);

                AddMethod?.Invoke(mTask);
                await Navigation.PopModalAsync(false);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", ex.Message, "Tamam");
            }
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            try
            {
                Result = false;
                await Navigation.PopModalAsync(false);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", ex.Message, "Tamam");
            }
        }
    }
}