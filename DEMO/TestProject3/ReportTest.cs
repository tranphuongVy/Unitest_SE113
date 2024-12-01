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
    public class Report_Test
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

        #region Test Tab Month
      
        [TestCase("10", "", "Please enter a Year!")]
        [TestCase("12", "2024","")]
        [TestCase("10", "2025", "Year exceeds the current year")]
        [TestCase("0", "2024", "Please enter Month from 1 to 12")]
        [TestCase("0000", "2024", "Please enter Month from 1 to 12")]
        [TestCase("10", "-2023", "Year cant be negative")]
        [TestCase("", "2023", "Please enter a Month!")]
        [TestCase("a", "2023", "Month error")]
        [TestCase("10", "202a", "Year error")]
        [TestCase("13", "2023", "Please enter Month from 1 to 12")]
        [TestCase("12 ", "2024", "")]
        [TestCase("   12", "2024 ", "")] //space
        [TestCase("012 ", "2024", "")]

        public void TestTabMonthValid(string month, string year, string expectedresult)
        {
            // Act
            var result = _report.ValidateInputTabMonth(month, year);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }
        #endregion

        #region Test Tab Year
        [TestCase("", "Please enter a Year!")]
        [TestCase("-2023", "Year cant be negative")]
        [TestCase("2025", "Year exceeds the current year")]
        [TestCase("20252", "Year exceeds the current year")]
        [TestCase("2024","")]
        [TestCase("  2024 ", "")] //space
        public void TestTabYearValid(string year, string expectedresult)
        {
            var result = _report.ValidateInputTabYear(year);
            Assert.That(result, Is.EqualTo(expectedresult));
        }
        #endregion

    }
}