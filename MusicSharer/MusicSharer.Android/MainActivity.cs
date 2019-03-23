using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MusicSharer.Services;
using Xamarin.Forms;

namespace MusicSharer.Droid
{
    [Activity(Label = "MusicSharer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataHost = "play.google.com",
        DataScheme = "https"
    )]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataHost = "music.yandex.ru",
        DataScheme = "https"
    )]
    public sealed class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            await OpenUrl();
        }

        private async Task OpenUrl()
        {
            if (Intent?.Action == Intent.ActionView)
            {
                var track = await new GoogleService().GetTrack(Intent.DataString);
                var url = await new YandexService().GetUrl(track);
                Device.OpenUri(new Uri(url));
            }
        }
    }
}