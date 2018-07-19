using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Windows;

// Find curent GPS location.
namespace GoogleJSON
{
    public class GPSLocation
    {
        GeoCoordinateWatcher watcher;
        public string longLat { get; set; }

        public void GetLocationEvent()
        {
            this.watcher = new GeoCoordinateWatcher();
            this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            bool started = this.watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));
            if (!started)
            {
                longLat = "GeoCoordinateWatcher timed out on start." + Environment.NewLine;
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            PrintPosition(e.Position.Location.Latitude, e.Position.Location.Longitude);
        }
        void PrintPosition(double Latitude, double Longitude)
        {
            longLat = string.Format("{0},{1}", Latitude, Longitude); 
        }

        public override string ToString()
        {
            return string.Format("{0}", longLat);
        }
    }
}
