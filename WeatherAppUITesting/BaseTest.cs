using FlaUI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace WeatherAppUITesting
{
    [TestClass]
    public class BaseTest
    {
        protected Application WPFApp;

        [TestInitialize]
        public void TestInitialize()
        {
            WPFApp = Application.Launch(@"..\..\..\WeatherAppWPF\bin\Debug\WeatherAppWPF.exe");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WPFApp.Close();
            File.Delete($"{Environment.CurrentDirectory}\\WeatherApp.json");
        }

    }
}
