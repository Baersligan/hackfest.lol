using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using beerbingo.Models;
using beerbingo.Views;
using beerbingo.Services;
using System.Collections.Generic;
using Android.Content.Res;

namespace beerbingo.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<ItemOLD> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Beer Bingo";
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
                List<string> places = new List<string>();
                List<string> beers = new List<string>();
                for (var i = 0; i < beerItems.Count; i++) 
                {
                    if (beers.Contains(beerItems[i].beer.beer_name) || places.Contains(beerItems[i].venue.venue_name) || beerItems[i].venue.location.venue_address == "") 
                    { 
                        continue;  
                    }

                    places.Add(beerItems[i].venue.venue_name);
                    beers.Add(beerItems[i].beer.beer_name);
                    string beer = beerItems[i].beer.beer_name;
                    string venue = beerItems[i].venue.venue_name;
                    double lng = beerItems[i].venue.location.lng;
                    double lat = beerItems[i].venue.location.lat;
                    string venue_address = beerItems[i].venue.location.venue_address;
                    ItemOLD itemOld = new ItemOLD { Id = Guid.NewGuid().ToString(), Text = beer, Description = beerItems[i].venue.venue_name, Lat = lat, Lng = lng, Venue_address = venue_address};
                    Items.Add(itemOld);
                    await DataStore.AddItemAsync(itemOld);
                    if(Items.Count >= 4) 
                    {
                        return;
                    }
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