using System.Collections.ObjectModel;

namespace FİNALODEV;

public partial class HavaDurumuSayfasi : ContentPage
{
    private ObservableCollection<SehirHavaDurumu> cities { get; set; }



    public HavaDurumuSayfasi()
    {
        InitializeComponent();
        cities = new ObservableCollection<SehirHavaDurumu>();
        cityListView.ItemsSource = cities;


    }

    private async void OnAddCityClicked(object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("Add City", "Enter city name:");


        if (!string.IsNullOrWhiteSpace(sehir))
        {
            sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
            sehir = sehir.Replace('Ç', 'C');
            sehir = sehir.Replace('Ğ', 'G');
            sehir = sehir.Replace('İ', 'I');
            sehir = sehir.Replace('Ö', 'O');
            sehir = sehir.Replace('Ü', 'U');
            sehir = sehir.Replace('Ş', 'S');

            var cityWeather = new SehirHavaDurumu { Name = sehir };
            cities.Add(cityWeather);

        }

    }


    private async void OnCitySelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is SehirHavaDurumu selectedCity)
        {





            cityListView.SelectedItem = null;
        }
    }
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is SehirHavaDurumu cityToDelete)
        {
            cities.Remove(cityToDelete);
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {

    }
}


public class SehirHavaDurumu
{
    public string Name { get; set; }

    public string WeatherImage => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff"; // Replace with the actual URL of the weather image
    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";

}


