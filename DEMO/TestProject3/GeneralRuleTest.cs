﻿using BLL;
using DTO;
using GUI.View;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace General_Rule
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GeneralRuleTest
    {
        private Window10 _rule;
        [SetUp]
        public void Setup()
        {
            _rule = new Window10();
        }




        #region Test Has Special Charaters Fuction 
        //Return true if string has special charaters
        [TestCase("Noi Bai@", true)]
        [TestCase("Noi Bai", false)]
        [TestCase(" Noi Bai     ", false)] //space
        [TestCase("[Tan Son Nhat]", true)]
        [TestCase("Tân Sơn Nhất", false)]
        [TestCase("Tan So,n Nhat", true)]

        public void TestHasSpecialChar(string input, bool expectedresult)
        {
            // Act

            var result = _rule.HasSpecialCharactersCheck(input);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }
        #endregion

        #region Test Format String Fuction 

        [TestCase("", "")]
        [TestCase("Noi Bai", "Noi Bai")]
        [TestCase("tan son nhat", "Tan Son Nhat")]
        [TestCase("TAN SON NHAT", "Tan Son Nhat")]
        [TestCase("Tan son Nhat", "Tan Son Nhat")]

        public void TestFormatString(string input, string expectedresult)
        {
            // Act

            var result = _rule.FormatStringCheck(input);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }
        #endregion

        #region Test Positive Number
        
        [TestCase("12", true)]
        [TestCase("      12 ", true)] //space
        [TestCase("-12", false)]
        [TestCase("tan son nhat", false)]
        [TestCase("1.2", true)]
        [TestCase("012", true)]


        public void TestPositiveIntergerChecking(string input, bool expectedresult)
        {
            // Act

            var result = _rule.PositiveIntegerCheck(input);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }
        #endregion

        #region Test Empty And Character- New Multiplier

        [TestCase("12a", "Please re-enter the Multiplier")]
        [TestCase("a", "Please re-enter the Multiplier")]
        [TestCase("", "")]
        [TestCase("12", "")]
        [TestCase("02", "")]
        [TestCase("022", "")]
        [TestCase(" 12 ", "")] //space
        [TestCase("11 g 12", "Please re-enter the Multiplier")]


        public void TestMultiplierChartactersCheck(string input, string expectedresult)
        {
            // Act

            var result = _rule.EmptyAndCharNewMultiplierCheck(input);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }
        #endregion

        #region Test Input
        [TestCase("Thuong Gi@", "2", "Ticket class' name has special character")]
        [TestCase("Thuong Gia", "-12", "Ticket class'multiplier must be > 0")]
        [TestCase("", "2", "Please enter the ticket class. Error")]
        [TestCase("Thuong Gia", " ", "Please enter the multiplier of the ticket class. Error")]
        [TestCase("Thuong Gia", "2.5", "All Correct")]
        [TestCase("Thuong Gia", "05", "All Correct")]
        [TestCase("Thuong Gia", "0", "Ticket class'multiplier must be > 0")]
        [TestCase("Thuong Gia", "000", "Ticket class'multiplier must be > 0")]
        [TestCase(" Thuong Gia ", "2.5", "All Correct")] //space
        [TestCase("Thuong Gia", " 2.5 ", "All Correct")] //space
        [TestCase(" Thuong Gia ", "05", "All Correct")]

        public void TestInputTicketClassAndMultipilerCheck(string inputTicketClass, string inputMultiplier, string expectedresult)
        {


            var result = _rule.InputTicketClassAndMultipilerCheck(inputTicketClass, inputMultiplier);

            // Assert
            Assert.That(result, Is.EqualTo(expectedresult));
        }
        #endregion
    }

}