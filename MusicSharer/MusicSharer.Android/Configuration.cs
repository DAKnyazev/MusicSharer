using System;
using System.Collections.Generic;
using Android.Content.PM;
using MusicSharer.Common.Interfaces;
using MusicSharer.Services;

namespace MusicSharer.Droid
{
    public static class Configuration
    {
        private static readonly GoogleService GoogleService = new GoogleService();
        private static readonly YandexService YandexService = new YandexService();

        public static Dictionary<string, IMusicService> MusicServiceDictionary = new Dictionary<string, IMusicService>
        {
            { GoogleService.HostUrl, GoogleService },
            { YandexService.HostUrl, YandexService }
        };

        public static IMusicService[] MusicServices = { GoogleService, YandexService };

        public static IMusicService GetInstalledMusicService(PackageManager packageManager)
        {
            foreach (var musicService in MusicServices)
            {
                if (IsServiceInstalled(packageManager, musicService.PackageName))
                {
                    return musicService;
                }
            }

            return new YandexService();
        }

        private static bool IsServiceInstalled(PackageManager packageManager, string packageName)
        {
            try
            {
                var package = packageManager.GetPackageInfo(packageName, PackageInfoFlags.Activities);

                return package?.ApplicationInfo?.Enabled ?? false;
            }
            catch
            {
                // ignored
            }

            return false;
        }
    }
}