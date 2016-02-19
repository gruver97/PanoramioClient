using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PanoramioClient.Model;
using HttpClient = Windows.Web.Http.HttpClient;

namespace PanoramioClient.Services
{
    public class PanoramioService : IPanoramioService, IDisposable
    {
        private bool _disposed;
        private readonly HttpClient _httpClient;
        private const string PanoramioBaseAddress = "http://www.panoramio.com";

        public PanoramioService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new HttpContentCodingWithQualityHeaderValue("utf-8"));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<string> GetImagesUrlAsync(double minX, double maxX, double minY, double maxY)
        {
            const string methodPart = "/map/get_panoramas.php?set=full&from=0&to=20&minx={0}&miny={1}&maxx={2}&maxy={3}&size=medium&mapfilter=true";
            try
            {
                var requestAddress = new Uri(new Uri(PanoramioBaseAddress), string.Format(methodPart,minX,minY,maxX,maxY));
                var response = await _httpClient.GetAsync(requestAddress);
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var images = JObject.Parse(jsonContent)["photos"].ToObject<IEnumerable<PhotoDescription>>();
                    return images?.FirstOrDefault().PhotoFileUrl;
                }
                return null;
            }
            catch (HttpRequestException httpRequestException)
            {
                
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _httpClient.Dispose();
            }
            _disposed = true;
        }

        ~PanoramioService()
        {
            Dispose(false);
        }
    }
}