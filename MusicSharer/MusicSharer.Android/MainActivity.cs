using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using MusicSharer.Services;
using Xamarin.Forms;

namespace MusicSharer.Droid
{
    [Activity(Label = "MusicSharer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataHost = GoogleService.HostUrl,
        DataScheme = "https"
    )]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataHost = YandexService.HostUrl,
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
            var intent = Intent;
            var dataHost = intent?.Data?.Host;
            if (intent?.Action == Intent.ActionView && !string.IsNullOrWhiteSpace(dataHost))
            {
                var url = Intent.DataString;
                var sourceService = Configuration.MusicServiceDictionary[dataHost];
                if (sourceService != null)
                {
                    var track = await sourceService.GetTrack(Intent.DataString);
                    if (track != null)
                    {
                        var installedService = Configuration.GetInstalledMusicService(PackageManager);
                        if (installedService != null)
                        {
                            url = await installedService.GetUrl(track);
                        }
                    }
                }
                Device.OpenUri(new Uri(url));
            }
        }
    }
}