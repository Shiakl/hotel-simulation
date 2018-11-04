using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationSE5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationSE5.EntityTests
{
    [TestClass()]
    public class EventadapterEntityTests
    {
        [TestMethod()]
        public void Event_HandlerTestCase1()
        {
            // Arrange
            List<char> data_value = new List<char>();
            string teststring = "HandlerTest3";
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("Item1", teststring);

            // Act
            foreach (var value in item)
            {
                data_value = value.Value.SkipWhile(c => !Char.IsDigit(c))
                .TakeWhile(Char.IsDigit)
                .ToList();
            }
            int classification = Convert.ToInt32(data_value.FirstOrDefault()) - '0';

            // Assert
            Assert.AreEqual(3, classification);
        }

        [TestMethod()]
        public void Event_HandlerTestCase3()
        {
            // Arrange
            List<int> event_values = new List<int>();
            string teststring = "21";
            int expected = 21;
            Dictionary<string, string> item = new Dictionary<string, string>();
            item.Add("Item1", teststring);

            // Act
            foreach (var value in item)
            {
                event_values.Add(Convert.ToInt32(value.Value));
            }

            // Assert
            Assert.AreEqual(expected, event_values.First());
        }
    }
}