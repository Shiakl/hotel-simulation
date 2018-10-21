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
        #region Building.cs
        [TestMethod]
        public void AssignRoomWithExistingID()
        {
            // Arrange
            var MySegment = new GuestRoom();
            var value = 3;
            // Act
            GuestRoom test = Building.AssignRoom(value);
            // Asset

        }

        [TestMethod]
        public void AssignRoomWithoutExistingID()
        {
            // Arrange
            var MySegment = new GuestRoom();
            var value = 3;
            // Act
            GuestRoom test = Building.AssignRoom(value);
            // Asset

        }

        [TestMethod]
        public void ReloadAvailableRooms()
        {
            // Arrange
            
            // Act
            
            // Asset

        }
        #endregion
        #region Node.cs

        #endregion
    }
}

