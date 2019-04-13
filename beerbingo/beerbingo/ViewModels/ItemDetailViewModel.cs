using System;
using System.Diagnostics;
using beerbingo.Models;

namespace beerbingo.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemOLD Item { get; set; }
        public ItemDetailViewModel(ItemOLD item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
