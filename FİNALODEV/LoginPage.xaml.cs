using FİNALODEV.MODEL;

namespace FİNALODEV;
public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void Reg_Clicked(object sender, EventArgs e)
    {
        if (loginStack.IsVisible)
        {
            loginStack.IsVisible = false;
            registerStack.IsVisible = true;
        }
        else
        {
            loginStack.IsVisible = true;
            registerStack.IsVisible = false;
        }
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        var (success, message) = await FirebaseServices.Login(lEmail.Text, lPassword.Text);
        if (success)
        {
            await Navigation.PopModalAsync();
        }
        else
            await DisplayAlert("Hata", message, "OK");
    }

    private async void Register_Clicked(object sender, EventArgs e)
    {
        var (success, message) = await FirebaseServices.Register(rDispName.Text, rEmail.Text, rPassword.Text);
        if (success)
        {
            await DisplayAlert("Başarılı", "Başarıyla kayıt oldunuz. Giriş yapabilirsiniz.", "Tamam");
            loginStack.IsVisible = true;
            registerStack.IsVisible = false;
        }
        else
        {
            await DisplayAlert("Hata", message, "Tamam");
        }
    }
}
