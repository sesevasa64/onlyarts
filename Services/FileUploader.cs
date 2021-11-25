using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace onlyarts.Services
{
    public class FileUploader
    {
        private HttpClient client;
        private string uri = "https://api.imgbb.com/1/upload?key=87d7e5f03b759c75fd11c22c7762e221";
        public FileUploader()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/form-data");
        }
        public Dictionary<String, object> UploadFile(string fileContent)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("image", fileContent)
            });
            var response = client.PostAsync(uri, formContent).GetAwaiter().GetResult();
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Dictionary<String, object>>(result);
        }
    }
}
