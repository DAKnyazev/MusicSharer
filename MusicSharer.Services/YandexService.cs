using MusicSharer.Common;
using System;
using System.Threading.Tasks;

namespace MusicSharer.Services
{
    public class YandexService : BaseService
    {
        public async Task<Track> GetTrack(string url)
        {
            try
            {
                var document = await OpenUrl(url);
            }
            catch (Exception e)
            {
            }

            return new Track();
        }

        public async Task<string> GetUrl(Track track)
        {
            return "https://music.yandex.ru/album/7131606/track/51151517";
        }
    }
}
