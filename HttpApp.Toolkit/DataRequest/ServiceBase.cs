using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpApp.Toolkit.DataRequest
{
    /// <summary>
    /// 数据获取通用类
    /// 作者：梅须白
    /// 时间：记不清了
    /// </summary>
    public class ServiceBase
    {
        private HttpResponseMessage response;
        private HttpClient httpClient;

        /// <summary>
        /// 泛型通用获取接口方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">接口url</param>
        /// <returns></returns>
        protected async Task<T> GetDataAsync<T>(string url)
        {
            httpClient = new HttpClient();

            var headers = httpClient.DefaultRequestHeaders;//获取每个请求标头的集合
            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            response = new HttpResponseMessage();

            try
            {
                response = await httpClient.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseText);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 泛型通用获取接口Post方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">接口url</param>
        /// <param name="content">Post内容</param>
        /// <returns></returns>
        protected async Task<T> PostDataAsync<T>(string url, Dictionary<string, string> param)
        {
            httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

            var headers = httpClient.DefaultRequestHeaders;//获取每个请求标头的集合
            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            response = new HttpResponseMessage();
            try
            {
                var httpContent = new FormUrlEncodedContent(param);
                response = await httpClient.PostAsync(new Uri(url), httpContent);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseText);

            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
