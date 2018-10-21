using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulationSE5;
using HotelSimulationSE5.HotelSegments;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {                      
        #region Node.cs

        #endregion
        #region Temproom.cs
        [TestMethod]
        public void Classification()
        {
            // Arrange
            
            // Act
            
            // Asset

        }
        #endregion

        #region Reception.cs
        [TestMethod]
        public void Assigned_Room_isTrue()
        {
            // Arrange
            Queue<string> WaitList = new Queue<string>();
            WaitList.Enqueue("Guest1");
            WaitList.Enqueue("Guest2");
            WaitList.Enqueue("Guest3");

            List<string> guestList = new List<string>();
            int waittime = 0;
            int waitduration = 5;
            int input = 6; 
            string expected = "Guest1";

            // Act
            for (int testcount = 0; testcount < input; testcount++)
            {
                if (WaitList.Any())
                {
                    if (waittime == waitduration)
                    {
                        waittime = 0;
                        guestList.Add(WaitList.First());
                        WaitList.Dequeue();
                    }
                    else
                    {
                        waittime++;
                    }
                }
            }

            // Assert
            Assert.AreEqual(guestList.First(), expected);
        }

        [TestMethod]
        public void Assigned_Room_isElse()
        {
            // Arrange
            Queue<string> WaitList = new Queue<string>();
            WaitList.Enqueue("Guest1");
            WaitList.Enqueue("Guest2");
            WaitList.Enqueue("Guest3");

            List<string> guestList = new List<string>();
            int waittime = 0;
            int waitduration = 5;
            int input = 6; ;
            string expected = "Guest2";

            // Act
            for (int testcount = 0; testcount <input;testcount++)
            {
                if (WaitList.Any())
                {
                    if (waittime == waitduration)
                    {
                        waittime = 0;
                        guestList.Add(WaitList.First());
                        WaitList.Dequeue();
                    }
                    else
                    {
                        waittime++;
                    }
                }
            }

            

            // Assert
            Assert.AreEqual(WaitList.First(), expected);
        }
        #endregion
    }
}

