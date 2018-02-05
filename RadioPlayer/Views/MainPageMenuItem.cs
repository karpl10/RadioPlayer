using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace RadioPlayer.Views
{

    public class MainPageMenuItem
    {
        public MainPageMenuItem()
        {
            //FavoriteImage = new Image{Source = ImageSource.FromResource("ic_favorite_border.png") };
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }
}