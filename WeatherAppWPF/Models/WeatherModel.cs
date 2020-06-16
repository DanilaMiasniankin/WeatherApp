using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppWPF.Models
{
    class WeatherModel : INotifyPropertyChanged
    {
        private string _cityTitle;
        private string _weatherInfo;

        public string CityTitle
        {
            get { return _cityTitle; }
            set
            {
                if (_cityTitle == value)
                    return;
                _cityTitle = value;
                OnPropertyChanged("CityTitle");
            }
        }
        public string WeatherInfo
        {
            get { return _weatherInfo; }
            set
            {
                if (_weatherInfo == value)
                    return;
                _weatherInfo = value;
                OnPropertyChanged("WeatherInfo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
