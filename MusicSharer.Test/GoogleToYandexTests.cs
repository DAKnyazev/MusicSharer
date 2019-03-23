using MusicSharer.Services;
using Xunit;

namespace MusicSharer.Test
{
    public class GoogleToYandexTests
    {
        private readonly string _googleTrackShareUrl = "https://play.google.com/music/m/Tdvi6klglbmmq7pqeklm6adbwju?t=Stay_Awake_-_Dean_Lewis";
        private readonly string _targetYandexUrl = "https://music.yandex.ru/album/7131606/track/51151517";

        [Fact]
        public async void Test1()
        {
            var service = new GoogleService();
            var track = await service.GetTrack(_googleTrackShareUrl);
            var yaService = new YandexService();
            var url = await yaService.GetUrl(track);
            
            Assert.NotNull(track);
        }
    }
}
