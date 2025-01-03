namespace FİNALODEV;

public partial class AyarlarSayfasi : ContentPage
{
    public AyarlarSayfasi()
    {
        InitializeComponent();
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            // Switch ON: Karanlık tema
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            // Switch OFF: Aydınlık tema
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }
}