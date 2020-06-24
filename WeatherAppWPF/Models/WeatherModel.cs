using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace WeatherAppWPF.Models
{
    class WeatherModel : INotifyPropertyChanged
    {
        private const string API_KEY = "apikey=1CqKZzD24kU5gD8NWQFCvz5wzGcWnvto";
        private const string LOCATION_REQUEST = "http://dataservice.accuweather.com/locations/v1/cities/search?" + API_KEY + "&q=";
        private const string CONDITIONS_REQUEST = "http://dataservice.accuweather.com/currentconditions/v1/";

        private string _cityTitle;
        public string WeatherInfo { get; set; }

        public string CityTitle
        {
            get { return _cityTitle; }
            set
            {
                if (_cityTitle == value)
                    return;
                _cityTitle = value;

                SetWeatherInfo(GetCityId(GetResponseJson(LOCATION_REQUEST + value.ToLower())));
                OnPropertyChanged("CityTitle");
                OnPropertyChanged("WeatherInfo");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetResponseJson(string request)
        {
            WebRequest locationRequest = WebRequest.Create($"{request}");
            WebResponse locationResponse = locationRequest.GetResponse();
            using (StreamReader stream = new StreamReader(locationResponse.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }
        }

        public string GetCityId(string response)
        {
            dynamic deserialized = JsonConvert.DeserializeObject(response);
            return deserialized.First.Key;
        }

        public void SetWeatherInfo(string cityId)
        {
            string conditions_request = $"{CONDITIONS_REQUEST}{cityId}?{API_KEY}";
            dynamic deserialized = JsonConvert.DeserializeObject(GetResponseJson(conditions_request));
            WeatherInfo = deserialized.First.WeatherText;
        }
    }
}
