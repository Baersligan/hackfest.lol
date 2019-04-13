using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace beerbingo.Services
{
    public class RESTService
    { 
        HttpClient _client;

  public RESTService()
        {
            _client = new HttpClient();
        }


        public async Task<bool> apiTest()
        {
            var APIstring = "https://api.untappd.com/v4/thepub/local?radius=1&dist_pref=km&limit=1&lat=59.304698&lng=18.078462&client_id=7CEE7753106952507C5DD070FE9DFFD1A726EF5A&client_secret=869F205117B831556ED3994CB3FD765F59D4968C";
            var uri = new Uri(string.Format(APIstring, string.Empty));
        
        var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                Debug.WriteLine(content);
            }

            return true;
}

    }

}
