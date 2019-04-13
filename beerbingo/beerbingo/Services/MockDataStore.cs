using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using beerbingo.Models;
using beerbingo.Views;
using Xamarin.Forms;

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

            RESTService.apiTest();

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