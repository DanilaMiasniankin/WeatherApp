using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.Core;

namespace WeatherAppUITesting
{
    [TestClass]
    public class Tests : BaseTest
    {
        /// <summary>
        /// This method checks that at the beginning list is empty
        /// </summary>
        [TestMethod]
        public void CheckRowsCountNumber()
        {
            Assert.IsTrue(weatherDataGrid.RowCount == 1);
        }

        /// <summary>
        /// This method checks the ability to add and remove cities
        /// </summary>
        [TestMethod]
        public void CheckAddingAndRemoving()
        {
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
            Thread.Sleep(2000);//waiting for correct input
            firstCell = weatherDataGrid.Rows.FirstOrDefault().Cells.FirstOrDefault().AsGridCell();
            Assert.IsTrue(weatherDataGrid.RowCount == 2);
            firstCell.Click();
            Keyboard.Press(VirtualKeyShort.DELETE);
            Thread.Sleep(2000);//waiting for removal
            Assert.IsTrue(weatherDataGrid.RowCount == 1);
        }

        /// <summary>
        /// This method checks the ability to save cities to the list
        /// </summary>
        [TestMethod]
        public void CheckResultSaving()
        {
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
            Thread.Sleep(2000);//waiting for correct input

            Assert.IsTrue(weatherDataGrid.RowCount == 2);
            Assert.IsTrue(weatherDataGrid.Rows.FirstOrDefault().Cells.FirstOrDefault().Value.Equals("kharkiv"));

            WPFApp.Close();
            WPFApp = Application.Launch(@"..\..\..\WeatherAppWPF\bin\Debug\WeatherAppWPF.exe");
            using (var automation = new UIA3Automation())
            {
                window = WPFApp.GetMainWindow(automation);
                weatherDataGrid = window.FindFirstDescendant(element => element.ByAutomationId("WeatherGrid"))?.AsGrid();
                firstCell = weatherDataGrid.Rows.FirstOrDefault().Cells.FirstOrDefault().AsGridCell();
                var secondCell = weatherDataGrid.Rows.FirstOrDefault().Cells.Last().AsGridCell();
                Assert.IsTrue(firstCell.Value.Equals("kharkiv"));
                Assert.IsFalse(secondCell.Equals(string.Empty));
            }
        }
    }
}
