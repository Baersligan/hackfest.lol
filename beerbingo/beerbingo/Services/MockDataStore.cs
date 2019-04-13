using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using beerbingo.Models;
using Untappd.NetStandard.Authentication;
using Untappd.NetStandard.Request;
using Untappd.NetStandard.Responses.BeerSearch;
using Untappd.NetStandard.Responses.UserDistinctBeer;

namespace beerbingo.Services
{
    public class MockDataStore : IDataStore<ItemOLD>
    {
        List<ItemOLD> items;
        string ClientID = "7CEE7753106952507C5DD070FE9DFFD1A726EF5A";
        string ClientSecret = "869F205117B831556ED3994CB3FD765F59D4968C";

        public MockDataStore()
        {
            items = new List<ItemOLD>();
            var mockItems = new List<ItemOLD>
            {
                new ItemOLD { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new ItemOLD { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new ItemOLD { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new ItemOLD { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new ItemOLD { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new ItemOLD { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }

            var api = new RESTService();

            Task<bool> result = api.apiTest();
            UnAuthenticatedUntappdCredentials login = new UnAuthenticatedUntappdCredentials(ClientID, ClientSecret);
            IDictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("lat", 59.304698);
            dict.Add("lng",18.078462);
            dict.Add("limit", 1);

           // var res = new Repository().Get<UserDistinctBeers>(login, "https://api.untappd.com/v4/thepub/local", dict);
            var res = new Repository().Get<BeerSearch>(login, "https://api.untappd.com/v4/thepub/local", dict);
            Debug.WriteLine("HEJ");
           Debug.WriteLine(res.Response);


            //var res2 = new Repository().Get<>

        }

        public async Task<bool> AddItemAsync(ItemOLD item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ItemOLD item)
        {
            var oldItem = items.Where((ItemOLD arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ItemOLD arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemOLD> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemOLD>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}