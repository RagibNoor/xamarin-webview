using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CoreLocation;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(GeoLocationService))]
namespace OfflineApp.iOS.Implementations
{
    public class GeoLocationService : CLLocationManagerDelegate, IGeoLocationService
    {
        protected CLLocationManager locationManager;
        public async Task<IDictionary<string, string>> GetCurrentLocation()
        {

            IDictionary<string, string> locationDictionary = new Dictionary<string, string>();
            try
            {
                locationManager = new CLLocationManager();
                locationManager.RequestWhenInUseAuthorization();
                if (CLLocationManager.LocationServicesEnabled)
                {
                    locationManager.Delegate = this;
                    locationManager.DesiredAccuracy = 1;
                    locationManager.StartUpdatingLocation();
                }

                var locationManagerLocation = this.locationManager.Location;
                if (locationManagerLocation == null)
                {
                    locationDictionary.Add("errMsg", "Unable to Get Location.");
                    return locationDictionary;
                }

                var location = locationManagerLocation.Coordinate;
                
                locationDictionary.Add("Latitude", location.Latitude.ToString(CultureInfo.InvariantCulture));
                locationDictionary.Add("Longitude", location.Longitude.ToString(CultureInfo.InvariantCulture));

                return locationDictionary;
            }

            catch (FeatureNotSupportedException fnsEx)
            {
                locationDictionary.Add("errMsg", "Handle not supported on device exception");
                return locationDictionary;
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                locationDictionary.Add("errMsg", "Handle not enabled on device exception");
                return locationDictionary;
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                locationDictionary.Add("errMsg", "Handle permission exception");
                return locationDictionary;
                // Handle permission exception
            }
            catch (Exception ex)
            {
                locationDictionary.Add("errMsg", "Unable to Get Location");
                return locationDictionary;
                // Unable to get location
            }
        }
    }
}



