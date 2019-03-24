using System.Threading.Tasks;

namespace MusicSharer.Common.Interfaces
{
    public interface IMusicService
    {
        /// <summary>
        /// Имя сборки
        /// </summary>
        string PackageName { get; }

        /// <summary>
        /// Получить данные о треке
        /// </summary>
        /// <param name="url">Ссылка на трек</param>
        Task<Track> GetTrack(string url);

        /// <summary>
        /// Получить ссылку на трек
        /// </summary>
        /// <param name="track">Информация о треке</param>
        Task<string> GetUrl(Track track);
    }
}
