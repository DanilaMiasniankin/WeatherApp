using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.Core;
using FluentAssertions;
using FluentAssertions.Common;

namespace WeatherAppUITesting
{
    [TestClass]
    public class Tests : BaseTest
    {
        [TestMethod]
        public void CheckRowsCountNumber()
        {
            using (var automation = new UIA3Automation())
            {
                var window = WPFApp.GetMainWindow(automation);
                var weatherDataGrid = window.FindFirstDescendant(element => element.ByAutomationId("WeatherGrid"))?.AsGrid();
                Assert.IsTrue(weatherDataGrid.RowCount == 1);
            }
        }

        [TestMethod]
        public void CheckAddingAndRemoving()
        {
            using (var automation = new UIA3Automation())
            {
                var window = WPFApp.GetMainWindow(automation);
                var weatherDataGrid = window.FindFirstDescendant(element => element.ByAutomationId("WeatherGrid"))?.AsGrid();
                var firstCell = weatherDataGrid.Rows.FirstOrDefault().Cells.FirstOrDefault().AsGridCell();
                firstCell.DoubleClick();
                Keyboard.Press(VirtualKeyShort.KEY_K);
                Keyboard.Press(VirtualKeyShort.KEY_H);
                Keyboard.Press(VirtualKeyShort.KEY_A);
                Keyboard.Press(VirtualKeyShort.KEY_R);
                Keyboard.Press(VirtualKeyShort.KEY_K);
                Keyboard.Press(VirtualKeyShort.KEY_I);
                Keyboard.Press(VirtualKeyShort.KEY_V);
                Keyboard.Press(VirtualKeyShort.ENTER);
                Thread.Sleep(2000);
                firstCell = weatherDataGrid.Rows.FirstOrDefault().Cells.FirstOrDefault().AsGridCell();
                Assert.IsTrue(weatherDataGrid.RowCount == 2);
                firstCell.Click();
                Keyboard.Press(VirtualKeyShort.DELETE);
                Thread.Sleep(2000);
                Assert.IsTrue(weatherDataGrid.RowCount == 1);
            }
        }

        [TestMethod]
        public void CheckResultSaving()
        {
            using (var automation = new UIA3Automation())
            {
                var window = WPFApp.GetMainWindow(automation);
                var weatherDataGrid = window.FindFirstDescendant(element => element.ByAutomationId("WeatherGrid"))?.AsGrid();
                var firstCell = weatherDataGrid.Rows.FirstOrDefault().Cells.FirstOrDefault().AsGridCell();
                firstCell.DoubleClick();
                Keyboard.Press(VirtualKeyShort.KEY_K);
                Keyboard.Press(VirtualKeyShort.KEY_H);
                Keyboard.Press(VirtualKeyShort.KEY_A);
                Keyboard.Press(VirtualKeyShort.KEY_R);
                Keyboard.Press(VirtualKeyShort.KEY_K);
                Keyboard.Press(VirtualKeyShort.KEY_I);
                Keyboard.Press(VirtualKeyShort.KEY_V);
                Keyboard.Press(VirtualKeyShort.ENTER);
                Thread.Sleep(2000);
                Assert.IsTrue(weatherDataGrid.RowCount == 2);
                Assert.IsTrue(weatherDataGrid.Rows.FirstOrDefault().Cells.FirstOrDefault().Value.Equals("kharkiv"));
            }
            WPFApp.Close();
            WPFApp = Application.Launch(@"..\..\..\WeatherAppWPF\bin\Debug\WeatherAppWPF.exe");
            using (var automation = new UIA3Automation())
            {
                var window = WPFApp.GetMainWindow(automation);
                var weatherDataGrid = window.FindFirstDescendant(element => element.ByAutomationId("WeatherGrid"))?.AsGrid();
                var firstCell = weatherDataGrid.Rows.FirstOrDefault().Cells.FirstOrDefault().AsGridCell();
                var secondCell = weatherDataGrid.Rows.FirstOrDefault().Cells.Last().AsGridCell();
                Assert.IsTrue(firstCell.Value.Equals("kharkiv"));
                Assert.IsFalse(secondCell.Equals(string.Empty));
            }
        }
    }
}
