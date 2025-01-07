using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FİNALODEV.MODEL
{
    public class User
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public static class FirebaseServices
    {
        static FirebaseClient client = new FirebaseClient("https://gorselfinal-dba67-default-rtdb.firebaseio.com/");
        public static User CurrentUser { get; private set; }

        public static async Task<(bool success, string message)> Register(string name, string email, string password)
        {
            try
            {
                
                var existingUser = (await client
                    .Child("Users")
                    .OnceAsync<User>())
                    .FirstOrDefault(u => u.Object.Email == email);

                if (existingUser != null)
                    return (false, "Bu e-posta adresi zaten kayıtlı.");

               
                var newUser = new User
                {
                    DisplayName = name,
                    Email = email,
                    Password = password
                };

                await client.Child("Users").PostAsync(newUser);
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static async Task<(bool success, string message)> Login(string email, string password)
        {
            try
            {
                var users = await client
                    .Child("Users")
                    .OnceAsync<User>();

                var user = users.FirstOrDefault(u =>
                    u.Object.Email == email &&
                    u.Object.Password == password);

                if (user == null)
                    return (false, "E-posta veya şifre hatalı.");

                CurrentUser = user.Object;
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}