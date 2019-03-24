using System;
using System.Threading.Tasks;
using MusicSharer.Common;
using MusicSharer.Common.Interfaces;

namespace MusicSharer.Services
{
    public class GoogleService : BaseService, IMusicService
    {
        public GoogleService() : base("https://play.google.com/music/listen#/sr/")
        {
        }

        public const string HostUrl = "play.google.com";
        public string PackageName => "com.google.android.music";

        public async Task<Track> GetTrack(string url)
        {
            try
            {
                var searchTest = GetSearchParameters(url);
                var normalText = searchTest?.Replace("_", " ");
                var splitedStrings = normalText?.Split(new []{ " - " }, StringSplitOptions.RemoveEmptyEntries);

                if (splitedStrings?.Length == 2)
                {
                    return new Track
                    {
                        Author = splitedStrings[1].Trim(),
                        Name = splitedStrings[0].Trim()
                    };
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        public Task<string> GetUrl(Track track)
        {
            throw new NotImplementedException();
        }
    }
}
