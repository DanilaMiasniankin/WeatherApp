using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using WeatherAppWPF.Models;

namespace WeatherAppWPF.Services
{
    class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<WeatherModel> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<WeatherModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<WeatherModel>>(fileText);
            }
            
        }

        public void SaveData(object weatherDataList)
        {
            using(StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(weatherDataList);
                writer.Write(output);
            }
        }
    }
}
