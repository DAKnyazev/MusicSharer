using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MusicSharer.Services
{
    public abstract class BaseService
    {
        protected readonly string SearchUrl;

        protected BaseService(string searchUrl)
        {
            SearchUrl = searchUrl;
        }

        protected async Task<HtmlDocument> OpenUrl(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            var document = new HtmlDocument();
            document.Load(await response.Content.ReadAsStreamAsync());

            return document;
        }

        protected string GetSearchParameters(string url)
        {
            if (url == null)
            {
                return string.Empty;
            }

            var paramIndex = url.IndexOf("?", StringComparison.InvariantCultureIgnoreCase);
            if (paramIndex <= 0)
            {
                return string.Empty;
            }

            var valueIndex = url.IndexOf(
                "=",
                url.IndexOf("?", StringComparison.InvariantCultureIgnoreCase),
                StringComparison.InvariantCultureIgnoreCase);

            return url.Substring(valueIndex > 0 ? valueIndex + 1 : paramIndex + 1);
        }
    }
}
