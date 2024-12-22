using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SchoolProject.Services.Extensions
{
    public interface IHttpClientService<T>
    {
        public Task<T> MethodPost(object requestClass, string endpoint);
        public Task<T> MethodGet(string endpoint);
    }
    public class HttpClientService<T> : IHttpClientService<T>
    {
        public async Task<T> MethodPost(object requestClass, string endpoint)
        {
            try
            {
                using (HttpClient HttpClient = new HttpClient())
                {
                    HttpClient.Timeout = TimeSpan.FromMinutes(100);
                    var request = new StringContent(JsonConvert.SerializeObject(requestClass), System.Text.Encoding.UTF8, "application/json");
                    var response = await HttpClient.PostAsync(endpoint, request);
                    string T = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(T);
                }
            }
            catch (Exception e)
            {

            }

            return JsonConvert.DeserializeObject<T>("");
        }
        public async Task<T> MethodGet(string endpoint)
        {
            using (HttpClient HttpClient = new HttpClient())
            {
                HttpClient.Timeout = TimeSpan.FromMinutes(10);
                var response = await HttpClient.GetAsync(endpoint);
                string T = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(T);
            }
        }
    }
}
