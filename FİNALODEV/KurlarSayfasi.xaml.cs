using Newtonsoft.Json;
namespace FİNALODEV;
public static class Servisler
{
    public static async Task<string> GetAltinDovizGuncelKurlar()
    {
        HttpClient client = new HttpClient();
        string url = "https://finans.truncgil.com/today.json";
        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string jsondata = await response.Content.ReadAsStringAsync();
        return jsondata;
    }
}
public partial class KurlarSayfasi : ContentPage
{
    public KurlarSayfasi()
    {
        InitializeComponent();
        LoadExchangeRatesAsync();
    }
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await LoadExchangeRatesAsync();
    }
    private async Task LoadExchangeRatesAsync()
    {
        try
        {
            string jsonData = await Servisler.GetAltinDovizGuncelKurlar();
            var rates = JsonConvert.DeserializeObject<Root>(jsonData);
            UpdateUI(rates);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Kurlar yüklenirken hata oluştu: " + ex.Message, "Tamam");
        }
    }
    private void UpdateUI(Root rates)
    {
        DolarAlis.Text = rates.USD.Alış;
        Dolarsatis.Text = rates.USD.Satış;
        DolarFark.Text = rates.USD.Değişim;
        DolarYonu.Source = GetDirectionImage(rates.USD.Değişim);

        EuroAlis.Text = rates.EUR.Alış;
        Eurosatis.Text = rates.EUR.Satış;
        EuroFark.Text = rates.EUR.Değişim;
        EuroYonu.Source = GetDirectionImage(rates.EUR.Değişim);

        SterlinAlis.Text = rates.GBP.Alış;
        Sterlinsatis.Text = rates.GBP.Satış;
        SterlinFark.Text = rates.GBP.Değişim;
        SterlinYonu.Source = GetDirectionImage(rates.GBP.Değişim);

        AltinAlis.Text = rates.gram_altin.Alış;
        Altinsatis.Text = rates.gram_altin.Satış;
        AltinFark.Text = rates.gram_altin.Değişim;
        AltinYonu.Source = GetDirectionImage(rates.gram_altin.Değişim);

        cAltinAlis.Text = rates.ceyrek_altin.Alış;
        cAltinsatis.Text = rates.ceyrek_altin.Satış;
        cAltinFark.Text = rates.ceyrek_altin.Değişim;
        cAltinYonu.Source = GetDirectionImage(rates.ceyrek_altin.Değişim);

        GumusAlis.Text = rates.gumus.Alış;
        Gumussatis.Text = rates.gumus.Satış;
        GumusFark.Text = rates.gumus.Değişim;
        GumusYonu.Source = GetDirectionImage(rates.gumus.Değişim);

        PHPAlis.Text = rates.PHP.Alış;
        PHPSatis.Text = rates.PHP.Satış;
        PHPFark.Text = rates.PHP.Değişim;
        PHPYonu.Source = GetDirectionImage(rates.PHP.Değişim);

        KWDAlis.Text = rates.KWD.Alış;
        KWDSatis.Text = rates.KWD.Satış;
        KWDFark.Text = rates.KWD.Değişim;
        KWDYonu.Source = GetDirectionImage(rates.KWD.Değişim);

        QARAlis.Text = rates.QAR.Alış;
        QARSatis.Text = rates.QAR.Satış;
        QARFark.Text = rates.QAR.Değişim;
        QARYonu.Source = GetDirectionImage(rates.QAR.Değişim);
    }
    private string GetDirectionImage(string change)
    {
        if (string.IsNullOrEmpty(change)) return "down.png";
        return change.Contains("-") ? "down.png" : "up.png";
    }
    public class CurrencyItem
    {
        public string Alış { get; set; }
        public string Tür { get; set; }
        public string Satış { get; set; }
        public string Değişim { get; set; }
    }
    public class Root
    {
        public string Update_Date { get; set; }
        public CurrencyItem USD { get; set; }
        public CurrencyItem EUR { get; set; }
        public CurrencyItem GBP { get; set; }
        public CurrencyItem PHP { get; set; }
        public CurrencyItem KWD { get; set; }
        public CurrencyItem QAR { get; set; }
        [JsonProperty("gram-altin")]
        public CurrencyItem gram_altin { get; set; }
        [JsonProperty("ceyrek-altin")]
        public CurrencyItem ceyrek_altin { get; set; }
        public CurrencyItem gumus { get; set; }
    }
}