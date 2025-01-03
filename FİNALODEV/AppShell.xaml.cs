namespace FİNALODEV
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        private void Shell_Appearing(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoginPage());

            base.OnAppearing();

        }

    }
}
