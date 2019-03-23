using System;
using System.Threading.Tasks;
using MusicSharer.Common;
using MusicSharer.Common.Interfaces;

namespace MusicSharer.Services
{
    public class GoogleService : BaseService, IMusicService
    {
        private readonly string _selectedSongClass = "selected-song-row";

        public GoogleService() : base("https://play.google.com/music/listen#/sr/")
        {
        }

        public async Task<Track> GetTrack(string url)
        {
            try
            {
                var searchTest = GetSearchParameters(url);
                var normalText = searchTest?.Replace("_", " ");
                var splitedStrings = normalText?.Split('-');

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
