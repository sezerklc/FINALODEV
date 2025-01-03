using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace FİNALODEV;

public partial class HaberDetay : ContentPage
{
    Item haber;

    public HaberDetay(Item item)
    {
        InitializeComponent();
        haber = item;
        webView.Source = item.link;
    }

    private async void ShareClicked(object sender, EventArgs e)
    {
        await ShareUri(haber.link, Share.Default);
    }

    public async Task ShareUri(string uri, IShare share)
    {
        await share.RequestAsync(new ShareTextRequest
        {
            Uri = uri,
            Title = haber.title
        });
    }

    private async void GeriClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    protected override bool OnBackButtonPressed()
    {
        Navigation.PopModalAsync();
        return true;
    }
}