using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace FİNALODEV.MODEL
{
    public class DL
    {
        static FirebaseClient client = new FirebaseClient("https://gorselfinal-dba67-default-rtdb.firebaseio.com/");

        public static async Task<(bool success, string message)> AddTask(Yapilacaklar task)
        {
            try
            {
                await client.Child("Tasks").PostAsync(task);
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static async Task<(bool success, string message)> EditTask(Yapilacaklar task)
        {
            try
            {
                var toUpdateTask = (await client
                    .Child("Tasks")
                    .OnceAsync<Yapilacaklar>())
                    .FirstOrDefault(a => a.Object.Id == task.Id);

                if (toUpdateTask != null)
                {
                    await client.Child($"Tasks/{toUpdateTask.Key}").PutAsync(task);
                    return (true, string.Empty);
                }
                return (false, "Görev bulunamadı");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static async Task<(bool success, string message)> DeleteTask(Yapilacaklar task)
        {
            try
            {
                var toDeleteTask = (await client
                    .Child("Tasks")
                    .OnceAsync<Yapilacaklar>())
                    .FirstOrDefault(a => a.Object.Id == task.Id);

                if (toDeleteTask != null)
                {
                    await client.Child($"Tasks/{toDeleteTask.Key}").DeleteAsync();
                    return (true, string.Empty);
                }
                return (false, "Görev bulunamadı");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static async Task<(ObservableCollection<Yapilacaklar> tasks, string message)> GetTasks()
        {
            try
            {
                var taskList = new ObservableCollection<Yapilacaklar>();
                var tasks = await client.Child("Tasks").OnceAsync<Yapilacaklar>();
                foreach (var task in tasks)
                {
                    taskList.Add(task.Object);
                }
                return (taskList, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}