using System.Collections.ObjectModel;

namespace FİNALODEV.MODEL
{
    public class BL
    {
        public static ObservableCollection<Yapilacaklar> Tasks { get; set; } = new ObservableCollection<Yapilacaklar>();

        public static async Task<(bool success, string message)> LoadTasks()
        {
            try
            {
                var (tasks, message) = await DL.GetTasks();
                if (tasks != null)
                {
                    Tasks.Clear();
                    foreach (var task in tasks)
                        Tasks.Add(task);
                    return (true, string.Empty);
                }
                return (false, message);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static async Task<(bool success, string message)> AddTask(Yapilacaklar task)
        {
            try
            {
                if (task != null)
                {
                    var (success, message) = await DL.AddTask(task);
                    if (success)
                    {
                        Tasks.Add(task);
                        return (true, string.Empty);
                    }
                    return (false, message);
                }
                return (false, "Görev boş olamaz");
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
                if (task != null)
                {
                    return await DL.EditTask(task);
                }
                return (false, "Görev boş olamaz");
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
                if (task != null)
                {
                    var (success, message) = await DL.DeleteTask(task);
                    if (success)
                    {
                        Tasks.Remove(task);
                        return (true, string.Empty);
                    }
                    return (false, message);
                }
                return (false, "Görev boş olamaz");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}