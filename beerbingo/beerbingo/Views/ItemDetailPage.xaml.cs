using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using beerbingo.Models;
using beerbingo.ViewModels;
using System.Diagnostics;

namespace beerbingo.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new ItemOLD
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
       
       void OnButtonClicked(object sender, EventArgs args)
        {
            Debug.Print("tjaaaaaaba"+ viewModel.Item.Lat);

            //Device.OpenUri(new Uri("https://www.google.com/maps/search/?api=1&query="+ viewModel.Item.Lat+", "+ viewModel.Item.Lng));
            Device.OpenUri(new Uri("https://maps.google.com/?q=" + viewModel.Item.Venue_address));

        }
    }
}