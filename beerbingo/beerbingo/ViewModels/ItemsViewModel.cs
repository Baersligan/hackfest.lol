using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using beerbingo.Models;
using beerbingo.Views;
using beerbingo.Services;
using System.Collections.Generic;

namespace beerbingo.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<ItemOLD> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<ItemOLD>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            RESTService.Done += GotResult;
        }

        private void GotResult(RootObject untappdResult)
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, ItemOLD>(this, "AddItem", async (obj, item) =>
            {
                List<Item> beerItems = untappdResult.response.checkins.items;
                Debug.WriteLine(untappdResult.response.checkins.count);
                for(var i = 0; i < 4; i++) 
                {
                    ItemOLD itemOld = new ItemOLD { Id = Guid.NewGuid().ToString(), Text = beerItems[i].beer.beer_name, Description = beerItems[i].beer.beer_style };
                    Items.Add(itemOld);
                    await DataStore.AddItemAsync(itemOld);
                }
            });

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}