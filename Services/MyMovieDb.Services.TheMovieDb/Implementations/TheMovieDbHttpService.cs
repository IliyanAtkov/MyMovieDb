using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyMovieDb.Services.Constants;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyMovieDb.Services.TheMovieDb.Implementations
{
    public class TheMovieDbHttpService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<TheMovieDbHttpService> logger;

        public TheMovieDbHttpService(HttpClient httpClient, ILogger<TheMovieDbHttpService> logger)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.logger = logger;
        }

        public async Task<HttpServiceResult<T>> Get<T>(string url, Dictionary<string, string>? queryParams = null) where T : class, IServiceResult
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var serviceResult = new HttpServiceResult<T>();
            try
            {
                url = BuildQueryParams(queryParams, url);

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                using var res = await httpClient.SendAsync(request);
                if (res.IsSuccessStatusCode)
                {
                    serviceResult.IsSucess = true;
                    serviceResult.Result = await res.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    serviceResult.IsSucess = false;
                    serviceResult.Error = await res.Content.ReadFromJsonAsync<HttpServiceError>();
                }

                return serviceResult;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                serviceResult.IsSucess = false;
                serviceResult.Error = new HttpServiceError()
                {
                    StatusMessage = "Exception occured. Check the logs",
                    StatusCode = 500
                };

                return serviceResult;
            }
        }

        private string BuildQueryParams(Dictionary<string, string>? queryParams, string url)
        {
            if (queryParams == null || queryParams.Count == 0)
            {
                return url;
            }

            var sb = new StringBuilder(url);

            sb.Append("?");
            foreach (var queryParam in queryParams)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }

                sb.Append(queryParam.Key);
                sb.Append("=");
                sb.Append(WebUtility.UrlEncode(queryParam.Value));
            }

            return sb.ToString();
        }
    }
}