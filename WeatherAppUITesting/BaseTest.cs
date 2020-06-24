using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace WeatherAppUITesting
{
    [TestClass]
    public class BaseTest
    {
        protected Application WPFApp;
        protected Window window;
        protected Grid weatherDataGrid;

        /// <summary>
        /// This method launches app, initializes main window and weather grid
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            WPFApp = Application.Launch(@"..\..\..\WeatherAppWPF\bin\Debug\WeatherAppWPF.exe");
            using (var automation = new UIA3Automation())
            {
                window = WPFApp.GetMainWindow(automation);
                weatherDataGrid = window.FindFirstDescendant(element => element.ByAutomationId("WeatherGrid"))?.AsGrid();
            }
        }

        /// <summary>
        /// This method closes app and deletes json with saved cities
        /// </summary>
        [TestCleanup]
        public void TestCleanup()//closing app and deleting 
        {
            WPFApp.Close();
            File.Delete($"{Environment.CurrentDirectory}\\WeatherApp.json");
        }

    }
}
