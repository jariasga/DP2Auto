using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
    public class EnhancedMap: Map
    {
        public event EventHandler<MapTapEventArgs> Tapped;


        public EnhancedMap()
        {
            
        }

        public EnhancedMap(MapSpan region) : base(region)
        {

        }

        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { Position = coordinate});
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            var handler = Tapped;

            if (handler != null)
                handler(this, e);
        }
    }

    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}
