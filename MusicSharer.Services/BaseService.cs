using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MusicSharer.Services
{
    public abstract class BaseService
    {
        protected async Task<HtmlDocument> OpenUrl(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            var document = new HtmlDocument();
            document.Load(await response.Content.ReadAsStreamAsync());

            return document;
        }
    }
}
