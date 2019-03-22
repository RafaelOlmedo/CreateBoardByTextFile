using HandleTrelloBoard.Repository.Constants;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HandleTrelloBoard.Repository.Repositories.Base
{
    public class BaseRepository
    {
        public const string baseURL = "https://trello.com/1";

        private static HttpClient ReturnHttpClient()
        {
            HttpClient client = new HttpClient();

            string Url = baseURL;
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public static HttpResponseMessage Post(string integrationType, object obj)
        {            
            string Url = baseURL + "/" + integrationType;
            HttpResponseMessage response = null;

            using (HttpClient client = ReturnHttpClient())
            {
                response = client.PostAsJsonAsync(Url, obj).Result;
            }

            return response;
        }
    }
}
