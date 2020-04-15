using System;
using Moodio.Extensions;
using NUnit.Framework;

namespace Moodio.Common.Tests
{
    public class DateTimeExtensionTests
    {


        [Test]
        public void Given_DateTime_Returns_Epoch_Seconds()
        {
            //arrange
            var d1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var d2 = new DateTime(2000, 6, 1, 8, 0, 0, DateTimeKind.Utc);
            var d3 = new DateTime(2010, 12, 1, 16, 0, 0, DateTimeKind.Utc);
            var d4 = new DateTime(2020, 12, 31, 23, 59, 59, DateTimeKind.Utc);

            //act
            var e1 = d1.ToEpoch();
            var e2 = d2.ToEpoch();
            var e3 = d3.ToEpoch();
            var e4 = d4.ToEpoch();

            //asset
            Assert.AreEqual(0, e1);
            Assert.AreEqual(959846400, e2);
            Assert.AreEqual(1291219200, e3);
            Assert.AreEqual(1609459199, e4);
        }

        [Test]
        public void Given_Epoch_Seconds_Returns_DateTime()
        {
            

            //arrange
            var d1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var d2 = new DateTime(2000, 6, 1, 8, 0, 0, DateTimeKind.Utc);
            var d3 = new DateTime(2010, 12, 1, 16, 0, 0, DateTimeKind.Utc);
            var d4 = new DateTime(2020, 12, 31, 23, 59, 59, DateTimeKind.Utc);

            //act
            var e1 = DateTimeExtensions.EpochToDateTime(0);
            var e2 = DateTimeExtensions.EpochToDateTime(959846400);
            var e3 = DateTimeExtensions.EpochToDateTime(1291219200);
            var e4 = DateTimeExtensions.EpochToDateTime(1609459199);

            //asset
            Assert.AreEqual(d1, e1);
            Assert.AreEqual(d2, e2);
            Assert.AreEqual(d3, e3);
            Assert.AreEqual(d4, e4);
        }
    }
}