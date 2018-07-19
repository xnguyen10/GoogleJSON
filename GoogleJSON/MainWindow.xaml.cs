using Newtonsoft.Json;
using System;
using System.Net;
using System.Windows;
using System.Device.Location;

namespace GoogleJSON
{
    /// <summary>
    /// App used to find distance and travel time from Start to End location.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtStart.Focus();
        }

        string longLat;
        GPSLocation gps = new GPSLocation();

        private void btnStartLocation_Click(object sender, RoutedEventArgs e)
        {
            txtResults.Text = "";
            txtDestination.Focus();
            longLat = gps.ToString();
            txtResults.Text += longLat + " : Longitude & Latitude test data.\r\n\r\n";

            CurrentLocation();
        }

        private void btnClear(object sender, RoutedEventArgs e)
        {
            txtStart.Clear();
            txtDestination.Clear();
            txtResults.Clear();
            txtDestination.Focus();
        }

        // Get current location from GPS
        private void CurrentLocation()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + longLat);

                try
                {
                    var json = web.DownloadString(url);
                    var Object = JsonConvert.DeserializeObject<LongLatInfo>(json);

                    LongLatInfo gps = Object;

                    txtStart.Text = gps.results[0].formatted_address;
                }
                catch (Exception ex)
                {
                    txtResults.Text = "Data Error.  " + ex.Message + Environment.NewLine + Environment.NewLine;
                }
            }
        }

        // Display End location data to textbox
        private void btnShowData_Click(object sender, RoutedEventArgs e)
        {
            string origin = txtStart.Text;
            string destination = txtDestination.Text;

            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&departure_time=now&key=AIzaSyDoCkjqileEyQrVo7kd4DIzLABWFC3S5w8");

                try
                {
                    var json = web.DownloadString(url);
                    var Object = JsonConvert.DeserializeObject<DirectionsInfo>(json);

                    DirectionsInfo info = Object;

                    txtResults.Text += "Start Address: " + info.routes[0].legs[0].start_address.ToString() + Environment.NewLine;
                    txtResults.Text += "End Address: " + info.routes[0].legs[0].end_address.ToString() + Environment.NewLine;
                    txtResults.Text += "Distance: " + info.routes[0].legs[0].distance.text.ToString() + Environment.NewLine;
                    txtResults.Text += "Duration: " + info.routes[0].legs[0].duration.text.ToString() + Environment.NewLine;
                    txtResults.Text += "Duration in Traffic: " + info.routes[0].legs[0].duration_In_Traffic.text.ToString() + Environment.NewLine + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    txtResults.Text = "Data Error.  " + ex.Message + Environment.NewLine + Environment.NewLine;             
                }
            }
        }    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gps.GetLocationEvent();         
        }
    }
}
