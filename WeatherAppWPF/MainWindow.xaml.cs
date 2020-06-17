﻿using System;
using System.ComponentModel;
using System.Windows;
using WeatherAppWPF.Models;
using WeatherAppWPF.Services;

namespace WeatherAppWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\WeatherApp.json";
        private BindingList<WeatherModel> _weatherDataList;
        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);
            try
            {
                _weatherDataList = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            
            dgCitiesList.ItemsSource = _weatherDataList;
            _weatherDataList.ListChanged += _weatherDataList_ListChanged;
        }

        private void _weatherDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType== ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted ||e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
    }
}
