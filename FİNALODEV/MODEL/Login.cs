using Firebase.Auth.Providers;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FİNALODEV.MODEL
{
    public static class FirebaseServices
    {
        static string projectId = "gorselfinal-dba67";

        static FirebaseAuthConfig config = new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyAl302MVGA8tghRG0P9DTZZvYVic7oVL78",
            AuthDomain = $"{projectId}.firebaseapp.com",
            Providers = new FirebaseAuthProvider[] { new EmailProvider() }
        };

        public static async Task<(bool success, string message)> Login(string mail, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass))
                {
                    return (false, "E-posta ve şifre alanlarını doldurunuz");
                }

                var client = new FirebaseAuthClient(config);
                var res = await client.SignInWithEmailAndPasswordAsync(mail, pass);
                return (true, "Giriş başarılı");
            }
            catch (Exception ex)
            {
                string turkceHata = ex.Message switch
                {
                    var msg when msg.Contains("INVALID_EMAIL") => "E-postanızı ve şifrenizi doğru girdiğinizden emin olun",
                    var msg when msg.Contains("EMAIL_NOT_FOUND") => "Bu e-posta adresi kayıtlı değil",
                    var msg when msg.Contains("INVALID_PASSWORD") => "Şifre hatalı",
                    var msg when msg.Contains("USER_DISABLED") => "Bu hesap devre dışı bırakılmış",
                    var msg when msg.Contains("TOO_MANY_ATTEMPTS_TRY_LATER") => "Çok fazla başarısız deneme. Lütfen daha sonra tekrar deneyin",
                    var msg when msg.Contains("NETWORK_ERROR") => "İnternet bağlantınızı kontrol edin",
                    _ => "Giriş yapılırken bir hata oluştu"
                };
                return (false, turkceHata);
            }
        }

        public static async Task<(bool success, string message)> Register(string name, string mail, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass))
                {
                    return (false, "Lütfen tüm alanları doldurunuz");
                }

                if (pass.Length < 6)
                {
                    return (false, "Şifre en az 6 karakter olmalıdır");
                }


                if (!mail.Contains("@") || !mail.Contains("."))
                {
                    return (false, "Geçerli bir e-posta adresi giriniz");
                }

                var client = new FirebaseAuthClient(config);
                var res = await client.CreateUserWithEmailAndPasswordAsync(mail, pass, name);
                return (true, "Kayıt başarılı");
            }
            catch (Exception ex)
            {
                string turkceHata = ex.Message switch
                {
                    var msg when msg.Contains("INVALID_EMAIL") => "Geçersiz e-posta formatı",
                    var msg when msg.Contains("EMAIL_EXISTS") => "Bu e-posta adresi zaten kayıtlı",
                    var msg when msg.Contains("WEAK_PASSWORD") => "Şifre çok zayıf. Daha güçlü bir şifre seçin",
                    var msg when msg.Contains("OPERATION_NOT_ALLOWED") => "E-posta/şifre ile kayıt şu anda devre dışı",
                    var msg when msg.Contains("TOO_MANY_ATTEMPTS_TRY_LATER") => "Çok fazla deneme yapıldı. Lütfen daha sonra tekrar deneyin",
                    var msg when msg.Contains("NETWORK_ERROR") => "İnternet bağlantınızı kontrol edin",
                    _ => "Kayıt olurken bir hata oluştu: " + ex.Message
                };
                return (false, turkceHata);
            }
        }
    }
}