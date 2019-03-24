using MusicSharer.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using MusicSharer.Common.Interfaces;

namespace MusicSharer.Services
{
    public class YandexService : BaseService, IMusicService
    {
        private readonly string _host = "https://music.yandex.ru";
        private readonly string _tracksDivClass = "serp-snippet__tracks";

        public YandexService() : base("https://music.yandex.ru/search?text=")
        {
        }

        public const string HostUrl = "music.yandex.ru";
        public string PackageName => "ru.yandex.music";

        public async Task<Track> GetTrack(string url)
        {
            try
            {
                var document = await OpenUrl(url);
            }
            catch
            {
                // ignored
            }

            return new Track();
        }

        public async Task<string> GetUrl(Track track)
        {
            var result = $"{SearchUrl}{track.Author} {track.Name}";
            try
            {
                var document = await OpenUrl(result);
                var trackNode = document.DocumentNode.SelectNodes($"//div[@class='{_tracksDivClass}']")?.FirstOrDefault();
                var path = trackNode?.SelectNodes($"//div[@class='d-track__name']")?.FirstOrDefault()?.FirstChild.Attributes["href"]?.Value;

                if (!string.IsNullOrWhiteSpace(path))
                {
                    return _host + path;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }
    }
}
