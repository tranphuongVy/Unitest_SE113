using BLL;
using DTO;
using GUI.View;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Windows.Controls;

namespace Report
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class Report_MonthTest
    {
        private Mock<ReportBLL> mockreportBLL;
        private ReportByMonthDTO reportByMonth;
        private Window5 _report;


        [SetUp]
        public void Setup()
        {
            mockreportBLL = new Mock<ReportBLL>();
            reportByMonth = new ReportByMonthDTO();
            _report = new Window5();
        }

        #region Test month
      
        [TestCase("10", "", "Please enter a Year!")]
        [TestCase("12", "2024","")]
        [TestCase("10", "2025", "Year exceeds the current year")]
        [TestCase("0", "2024", "Please enter Month from 1 to 12")]
        [TestCase("10", "-2023", "Year cant be negative")]
        [TestCase("", "2023", "Please enter a Month!")]
        #endregion
        public void TestTabMonth(string month, string year, string expectedresult)
        {
            // Act
            var result = _report.ValidateInputTabMonth(month, year);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }
    }
}